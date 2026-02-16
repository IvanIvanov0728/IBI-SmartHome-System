using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.LightingService
{
	public class LightingService : ILightingService
	{
		private readonly ApplicationDbContext _context;

		public LightingService(ApplicationDbContext context)
		{
			_context = context;
		}


		public LightingViewModel GetLightingViewModel()
		{
			var rooms = _context.Room
				.Select(r => new RoomViewModel
				{
					Id = r.Id,
					Name = r.Name,
					Floor = r.Floor
				})
				.ToList();

			List<LightControlViewModel> lights = _context.Lamps
				.Select(l => new LightControlViewModel
				{
					Id = l.Id,
					Name = l.Device.Room.Name,
					IsOn = l.IsOn,
					Brightness = l.Brightness,
					RoomId = l.Device.RoomId
				}).ToList();

			var viewModel = new LightingViewModel
			{
				Rooms = rooms,
				Lights = lights
			};
			return viewModel;
		}

		public bool UpdateLightState(int lightId, bool isOn)
		{
			var light = _context.Lamps.FirstOrDefault(l => l.Id == lightId);
			if (light == null)
				return false;
			light.IsOn = isOn;
			_context.SaveChanges();
			return true;
		}

		public bool UpdateLightBrightness(int lightId, int brightness)
		{
			var light = _context.Lamps.FirstOrDefault(l => l.Id == lightId);
			if (light == null)
				return false;
			light.Brightness = brightness;
			_context.SaveChanges();
			return true;
		}
	}
}
