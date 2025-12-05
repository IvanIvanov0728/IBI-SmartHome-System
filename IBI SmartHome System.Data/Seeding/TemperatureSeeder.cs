using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class TemperatureSeeder
	{
		public static IEnumerable<Temperature> Seed()
		{
			List<Temperature> tempSensorReadings = new List<Temperature>()
			{
				new Temperature()
				{
					Id = 1,
					DeviceId = 701,
					Timestamp = DateTime.UtcNow.AddHours(-5),
					TemperatureValue = 22.5f,
					Humidity = 45
				}
			};
			return tempSensorReadings;
		}
	}
}
