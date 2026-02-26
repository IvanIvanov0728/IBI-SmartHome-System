using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class ClimateScheduleSeeder
	{
		public static IEnumerable<ClimateSchedule> Seed()
		{
			int adminHouseId = 1;
			var schedules = new List<ClimateSchedule>
			{
				new ClimateSchedule { Id = 1, Day = "Mon-Fri", Time = "07:00 AM", Temp = "72°", Mode = "Heat", HouseId = adminHouseId },
				new ClimateSchedule { Id = 2, Day = "Mon-Fri", Time = "09:00 AM", Temp = "68°", Mode = "Eco", HouseId = adminHouseId },
				new ClimateSchedule { Id = 3, Day = "Mon-Fri", Time = "05:00 PM", Temp = "72°", Mode = "Heat", HouseId = adminHouseId },
				new ClimateSchedule { Id = 4, Day = "Sat-Sun", Time = "08:00 AM", Temp = "72°", Mode = "Heat", HouseId = adminHouseId },
				new ClimateSchedule { Id = 5, Day = "Sat-Sun", Time = "11:00 PM", Temp = "67°", Mode = "Sleep", HouseId = adminHouseId },
			};

			return schedules;
		}
	}
}
