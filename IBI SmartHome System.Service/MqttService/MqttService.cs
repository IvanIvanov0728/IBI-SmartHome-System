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
