using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace IBI_SmartHome_System.Service.MqttService
{
	public class MqttService : BackgroundService
	{
		private readonly IServiceProvider _sp;
		private readonly IConfiguration _config;
		private readonly ILogger<MqttService> _logger;
		private IMqttClient _client;

		public MqttService(IServiceProvider sp, IConfiguration config, ILogger<MqttService> logger)
		{
			_sp = sp;
			_config = config;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var host = _config["ConnectionForHive:Host"];
			var portStr = _config["ConnectionForHive:Port"];
			var username = _config["ConnectionForHive:Username"];
			var password = _config["ConnectionForHive:Password"];

			if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(portStr))
			{
				_logger.LogError("MQTT Configuration missing. MqttService will not start.");
				return;
			}

			if (!int.TryParse(portStr, out int port))
			{
				_logger.LogError("Invalid MQTT Port. MqttService will not start.");
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

				_logger.LogInformation("MQTT message received on topic {Topic}: {Payload}", topic, payload);

				try
				{
					using (var scope = _sp.CreateScope())
					{
						var handler = scope.ServiceProvider.GetRequiredService<IMqttMessageHandler>();
						await handler.HandleMessageAsync(topic, payload);
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error handling MQTT message on topic {Topic}", topic);
				}
			};

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					if (!_client.IsConnected)
					{
						_logger.LogInformation("Attempting to connect to MQTT broker at {Host}:{Port}...", host, port);
						await _client.ConnectAsync(options, stoppingToken);
						_logger.LogInformation("Connected to MQTT broker.");
						await _client.SubscribeAsync("esp32/#");
						_logger.LogInformation("Subscribed to esp32/# topic.");
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error connecting to MQTT broker. Retrying in 5 seconds...");
				}

				await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			if (_client != null && _client.IsConnected)
			{
				await _client.DisconnectAsync(cancellationToken: cancellationToken);
			}
			await base.StopAsync(cancellationToken);
		}
	}
}
