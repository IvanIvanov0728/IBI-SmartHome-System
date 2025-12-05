using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class MotionSensorSeeder
	{
		public static IEnumerable<MotionSensor> Seed()
		{
			List<MotionSensor> motionSensors = new List<MotionSensor>()
			{
				new MotionSensor()
				{
					Id = 1,
					DeviceId = 401,
					IsMotionDetected = false,
					SensitivityLevel = MotionSensorSensitivityLevel.High,
					BatteryLevel = 100.0,
					LastMotionDetected = null
				}
			};
			return motionSensors;
		}
	}
}
