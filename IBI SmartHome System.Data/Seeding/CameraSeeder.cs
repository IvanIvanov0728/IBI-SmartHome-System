using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class CameraSeeder
	{
		public static IEnumerable<Camera> Seed()
		{
			int adminHouseId = 1;

			var cameras = new List<Camera>
			{
				new Camera
				{
					Id = 1,
					HouseId = adminHouseId, // The house it belongs to
                    Name = "Front Porch Camera",
					StreamUrl = "https://example.com/stream/frontporch", // Placeholder URL
                    IsLive = true
				},
				new Camera
				{
					Id = 2,
					HouseId = adminHouseId, // The house it belongs to
                    Name = "Backyard Camera",
					StreamUrl = "https://example.com/stream/backyard", // Placeholder URL
                    IsLive = false
				}
			};
			return cameras;
		}
	}
}
