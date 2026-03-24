using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace IBI_SmartHome_System.Service.MqttService
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
			var host = _config["ConnectionForHive:Host"];
			var portStr = _config["ConnectionForHive:Port"];
			var username = _config["ConnectionForHive:Username"];
			var password = _config["ConnectionForHive:Password"];

			if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(portStr))
			{
				Console.WriteLine("MQTT Configuration missing. MqttService will not start.");
				return;
			}

			if (!int.TryParse(portStr, out int port))
			{
				Console.WriteLine("Invalid MQTT Port. MqttService will not start.");
				return;
			}

			var factory = new MqttFactory();
			_client = factory.CreateMqttClient();

			var optionsBuilder = new MqttClientOptionsBuilder()
				.WithTcpServer(host, port)
				.WithCredentials(username, password);

			if (port == 8883) // Common TLS port for HiveMQ
			{
				optionsBuilder.WithTls();
			}

			var options = optionsBuilder.Build();

			_client.ApplicationMessageReceivedAsync += async e =>
			{
				var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
				var topic = e.ApplicationMessage.Topic;

				using (var scope = _sp.CreateScope())
				{
					var handler = scope.ServiceProvider.GetRequiredService<IMqttMessageHandler>();
					await handler.HandleMessageAsync(topic, payload);
				}
			};

			await _client.ConnectAsync(options, stoppingToken);

			await _client.SubscribeAsync("esp32/#");
		}
	}
}
