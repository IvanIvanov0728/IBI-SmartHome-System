using IBI_SmartHome_System.Data.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Entity
{
	public class MotionSensor
	{
		public int Id { get; set; }
		public int DeviceId { get; set; }
		public Device? Device { get; set; }

		public bool IsMotionDetected { get; set; }
		public MotionSensorSensitivityLevel SensitivityLevel { get; set; }
		public double BatteryLevel { get; set; }
		public DateTime? LastMotionDetected { get; set; }
	}
}
