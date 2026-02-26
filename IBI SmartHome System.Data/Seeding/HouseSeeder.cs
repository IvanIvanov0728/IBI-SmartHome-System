using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class HouseSeeder
	{
		public static IEnumerable<House> Seed()
		{
			string adminId = "8e445865-a24d-4543-a6c6-9443d048cdb9"; // Fixed GUID matching IdentitySeeder

			var houses = new List<House>
			{
				new House
				{
					Id = 1,
					Name = "Admin's Home",
					Address = "123 Smart Home Lane, Sofia",
					Latitude = 42.70,
					Longitude = 23.32,
					UserId = adminId
				}
			};
			return houses;
		}
	}
}
