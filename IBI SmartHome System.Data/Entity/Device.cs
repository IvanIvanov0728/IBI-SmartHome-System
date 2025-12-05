using IBI_SmartHome_System.Data.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class Device
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public DeviceType Type { get; set; }
		public string? MqttTopic { get; set; }

		public int RoomId { get; set; }
		public Room? Room { get; set; }

		public Lamp? Lamp { get; set; }
		public Temperature? Temperature { get; set; }
		public MotionSensor? MotionSensor { get; set; }

		public ICollection<MqttMessage> MqttMessages { get; set; } = new List<MqttMessage>();
	}
}
