using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.MqttService
{
	public class MqttMessageHandler : IMqttMessageHandler
	{
		private readonly IServiceProvider _sp;
		private readonly IHubContext<SmartHomeHub> _hubContext;

		public MqttMessageHandler(IServiceProvider sp, IHubContext<SmartHomeHub> hubContext)
		{
			_sp = sp;
			_hubContext = hubContext;
		}

		public async Task HandleMessageAsync(string topic, string payload)
		{
			using (var scope = _sp.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

				// 1. Handle Temperature
				if (topic == "esp32/temperature")
				{
					if (!double.TryParse(payload, out double temp))
					{
						Console.WriteLine("Invalid temperature payload.");
						return;
					}

					var temps = await db.Temperature
						.Include(t => t.Device)
						.ThenInclude(d => d.Room)
						.ToListAsync();

					if (!temps.Any())
					{
						Console.WriteLine("No seeded temperature records found.");
						return;
					}

					foreach (var item in temps)
					{
						item.TemperatureValue = temp;
						item.Timestamp = DateTime.UtcNow;

						Console.WriteLine($"UPDATED TEMPERATURE => {item.Device.Room.Name}: {temp}°C");

						await _hubContext.Clients.All.SendAsync("DeviceUpdated", new
						{
							deviceId = item.DeviceId,
							type = "Temperature",
							value = temp,
							roomName = item.Device.Room.Name
						});
					}

					db.MqttMessages.Add(new MqttMessage
					{
						Topic = topic,
						Payload = payload,
						ReceivedAt = DateTime.Now
					});

					await db.SaveChangesAsync();
				}

				// 2. Handle Humidity
				if (topic == "esp32/humidity")
				{
					if (!double.TryParse(payload, out double hum))
					{
						Console.WriteLine("Invalid humidity payload.");
						return;
					}

					var temps = await db.Temperature
						.Include(t => t.Device)
						.ThenInclude(d => d.Room)
						.ToListAsync();

					foreach (var item in temps)
					{
						item.Humidity = (int)hum;
						item.Timestamp = DateTime.UtcNow;

						Console.WriteLine($"UPDATED HUMIDITY => {item.Device.Room.Name}: {hum}%");

						await _hubContext.Clients.All.SendAsync("DeviceUpdated", new
						{
							deviceId = item.DeviceId,
							type = "Humidity",
							value = hum,
							roomName = item.Device.Room.Name
						});
					}

					db.MqttMessages.Add(new MqttMessage
					{
						Topic = topic,
						Payload = payload,
						ReceivedAt = DateTime.Now
					});

					await db.SaveChangesAsync();
				}

				// 3. Handle Motion
				if (topic == "esp32/MotionSensor" || topic == "esp32/motion")
				{
					var sensors = await db.MotionSensor
						.Include(t => t.Device)
						.ThenInclude(d => d.Room)
						.ToListAsync();

					if (!sensors.Any())
					{
						Console.WriteLine("No seeded sensor records found.");
						return;
					}

					bool isDetected = payload == "1";
					foreach (var item in sensors)
					{
						item.LastMotionDetected = DateTime.UtcNow;
						item.IsMotionDetected = isDetected;

						Console.WriteLine($"UPDATED MOTION => {item.Device.Room.Name}: {isDetected}");

						await _hubContext.Clients.All.SendAsync("DeviceUpdated", new
						{
							deviceId = item.DeviceId,
							type = "Motion",
							value = isDetected,
							roomName = item.Device.Room.Name
						});
					}

					db.MqttMessages.Add(new MqttMessage
					{
						Topic = topic,
						Payload = payload,
						ReceivedAt = DateTime.Now
					});

					await db.SaveChangesAsync();
				}
			}
		}
	}
}
