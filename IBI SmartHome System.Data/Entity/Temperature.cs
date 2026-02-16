using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class Temperature
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public Device? Device { get; set; }

		public double TemperatureValue { get; set; }
		public int TargetTemperature { get; set; }
		public int Humidity { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}
