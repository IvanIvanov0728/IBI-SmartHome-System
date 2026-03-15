using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.MqttService
{
	public interface IMqttMessageHandler
	{
		Task HandleMessageAsync(string topic, string payload);
	}
}
