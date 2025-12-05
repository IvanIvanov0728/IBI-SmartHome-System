using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Data.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class LampSeeder
	{
		public static IEnumerable<Lamp> Seed()
		{
			List<Lamp> lamp = new List<Lamp>
			{
				new Lamp
				{
					Id = 1,
					DeviceId = 101,
					IsOn = false,
					Brightness = 75,
					Color = LampColors.WarmWhite
				}
			};
			return lamp;
		}
	}
}
