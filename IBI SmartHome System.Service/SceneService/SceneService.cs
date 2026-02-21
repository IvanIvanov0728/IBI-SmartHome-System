using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.SceneService
{
	public class SceneService : ISceneService
	{
		private readonly ApplicationDbContext _context;

		public SceneService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task ExecuteSceneAsync(int sceneId)
		{
			var scene = await _context.Scenes
				.Include(s => s.SceneActions)
				.FirstOrDefaultAsync(s => s.Id == sceneId);

			if (scene == null)
			{
				return;
			}

			foreach (var action in scene.SceneActions)
			{
				var device = await _context.Device
					.Include(d => d.Lamp)
					.Include(d => d.Temperature)
					.FirstOrDefaultAsync(d => d.Id == action.DeviceId);

				if (device == null)
				{
					continue;
				}

				switch (device.Type)
				{
					case DeviceType.Lamp:
						if (device.Lamp != null)
						{
							switch (action.Property)
							{
								case "Power":
									device.Lamp.IsOn = bool.Parse(action.Value);
									break;
								case "Brightness":
									device.Lamp.Brightness = int.Parse(action.Value);
									break;
								case "Color":
									device.Lamp.Color = Enum.Parse<LampColors>(action.Value);
									break;
							}
						}
						break;
					case DeviceType.TemperatureSensor:
						if (device.Temperature != null)
						{
							switch (action.Property)
							{
								case "TemperatureValue":
									device.Temperature.TemperatureValue = double.Parse(action.Value);
									break;
							}
						}
						break;
				}
			}

			await _context.SaveChangesAsync();
		}
	}
}
