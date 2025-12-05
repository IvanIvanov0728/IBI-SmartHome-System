using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class MqttMessage
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public Device? Device { get; set; }

		public string Topic { get; set; } = string.Empty;
		public string Payload { get; set; } = string.Empty;
		public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
	}
}
