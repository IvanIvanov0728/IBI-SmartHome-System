using IBI_SmartHome_System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Data.Seeding
{
	public class SceneSeeder
	{
		public static IEnumerable<Scene> Seed()
		{
			int adminHouseId = 1;
			var scenes = new List<Scene>
			{
				new Scene { Id = 1, Name = "Arrive Home", HouseId = adminHouseId },
				new Scene { Id = 2, Name = "Leave Home", HouseId = adminHouseId },
				new Scene { Id = 3, Name = "Good Morning", HouseId = adminHouseId },
				new Scene { Id = 4, Name = "Good Night", HouseId = adminHouseId }
			};

			return scenes;
		}

		public static IEnumerable<SceneAction> SeedActions()
		{
			var sceneActions = new List<SceneAction>
			{
                // Arrive Home: Turn on all lights
                new SceneAction { Id = 1, SceneId = 1, DeviceId = 101, Property = "Power", Value = "true" },

                // Leave Home: Turn off all lights
                new SceneAction { Id = 2, SceneId = 2, DeviceId = 101, Property = "Power", Value = "false" },

                // Good Morning: Turn on all lights to bright
                new SceneAction { Id = 3, SceneId = 3, DeviceId = 101, Property = "Power", Value = "true" },
				new SceneAction { Id = 4, SceneId = 3, DeviceId = 101, Property = "Brightness", Value = "100" },
				new SceneAction { Id = 5, SceneId = 3, DeviceId = 101, Property = "Color", Value = "White" },


                // Good Night: Turn off all lights and lower thermostat
                new SceneAction { Id = 6, SceneId = 4, DeviceId = 101, Property = "Power", Value = "false" },
				new SceneAction { Id = 7, SceneId = 4, DeviceId = 701, Property = "TemperatureValue", Value = "18" }
			};

			return sceneActions;
		}
	}
}
