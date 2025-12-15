using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace IBI_SmartHome_System.Service
{
	public class MqttService : BackgroundService
	{
		private readonly IServiceProvider _sp;
		private IMqttClient _client;
		private readonly IConfiguration _config;

		public MqttService(IServiceProvider sp, IConfiguration config)
		{
			_sp = sp;
			_config = config;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var Host = _config["ConnectionForHive:Host"];
			var Port = int.Parse(_config["ConnectionForHive:Port"]);
			var Username = _config["ConnectionForHive:Username"];
			var Password = _config["ConnectionForHive:Password"];

			var factory = new MqttFactory();
			_client = factory.CreateMqttClient();

			var options = new MqttClientOptionsBuilder()
				.WithTcpServer(Host, Port)
				.WithCredentials(Username, Password)
				.WithTls()
				.Build();

			_client.ApplicationMessageReceivedAsync += async e =>
			{
				var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

				using (var scope = _sp.CreateScope())
				{
					var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

					if (e.ApplicationMessage.Topic == "esp32/temperature")
					{
						var parts = payload.Split(',');

						if (parts.Length != 2 ||
							!double.TryParse(parts[0], out double temp) ||
							!int.TryParse(parts[1], out int hum))
						{
							Console.WriteLine("Invalid payload format.");
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
							item.Humidity = hum;
							item.Timestamp = DateTime.UtcNow;

							Console.WriteLine(
								$"UPDATED => {item.Device.Room.Name}: {temp}°C / {hum}%");
						}

						db.MqttMessages.Add(new MqttMessage
						{
							Topic = e.ApplicationMessage.Topic,
							Payload = payload,
							ReceivedAt = DateTime.Now
						});

						Console.WriteLine($"Greating received: temp {parts[0]} and hum {parts[1]}");

						await db.SaveChangesAsync();
					}

					if (e.ApplicationMessage.Topic == "esp32/motion")
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

						foreach (var item in sensors)
						{
							item.LastMotionDetected = DateTime.UtcNow;
							item.IsMotionDetected = payload == "1" ? true : false;

							Console.WriteLine(
								$"UPDATED => {item.Device.Room.Name}: Ther is movment {item.IsMotionDetected}");
						}

						db.MqttMessages.Add(new MqttMessage
						{
							Topic = e.ApplicationMessage.Topic,
							Payload = payload,
							ReceivedAt = DateTime.Now
						});

						Console.WriteLine($"IS THER MOVMENT {payload}");

						await db.SaveChangesAsync();
					}

					try
					{
						await db.SaveChangesAsync();
					}
					catch (Exception ex)
					{
						Console.WriteLine("DB ERROR: " + ex.Message);
					}
				}

			};

			await _client.ConnectAsync(options, stoppingToken);

			await _client.SubscribeAsync("esp32/#");
		}
	}
}
