using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class ActivityLogEntrySeeder
	{
		public static IEnumerable<ActivityLogEntry> Seed()
		{
			int adminHouseId = 1;

			var logEntries = new List<ActivityLogEntry>
			{
				new ActivityLogEntry
				{
					Id = 1,
					HouseId = adminHouseId,
					Timestamp = DateTime.UtcNow.AddMinutes(-5),
					Event = "Front Door Locked",
					Type = "info",
					DeviceId = 801 // Front Door
        		},
				new ActivityLogEntry
				{
					Id = 2,
					HouseId = adminHouseId,
					Timestamp = DateTime.UtcNow.AddMinutes(-15),
					Event = "Motion Detected at Front Porch",
					Type = "warning"
				},
				new ActivityLogEntry
				{
					Id = 3,
					HouseId = adminHouseId,
					Timestamp = DateTime.UtcNow.AddMinutes(-30),
					Event = "Back Door Unlocked",
					Type = "info",
					DeviceId = 802 // Back Door
        		},
				new ActivityLogEntry
				{
					Id = 4,
					HouseId = adminHouseId,
					Timestamp = DateTime.UtcNow.AddHours(-1),
					Event = "Admin Logged In",
					Type = "success"
				}
			};
			return logEntries;
		}
	}
}
