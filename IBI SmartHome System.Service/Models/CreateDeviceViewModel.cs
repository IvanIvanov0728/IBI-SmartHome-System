using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.Models
{
	public class CreateDeviceViewModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Type { get; set; } // e.g., "Lamp", "Thermostat", "Sensor"

		[Required]
		public int RoomId { get; set; }

		public string MqttTopic { get; set; }
	}
}
