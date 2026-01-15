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
		public LightingViewModel GetLightingViewModel()
		{
			var viewModel = new LightingViewModel
			{
				Lights = new List<LightControlViewModel>
				{
					new LightControlViewModel { Id = 1, Name = "Living Room Main Chandelier", IsOn = true, Brightness = 80 },
					new LightControlViewModel { Id = 2, Name = "Living Room Floor Lamp", IsOn = true, Brightness = 40 },
					new LightControlViewModel { Id = 3, Name = "Living Room Cove Lights", IsOn = true, Brightness = 60 },
					new LightControlViewModel { Id = 4, Name = "Kitchen Island Pendants", IsOn = false, Brightness = 100 },
					new LightControlViewModel { Id = 5, Name = "Kitchen Under Cabinet", IsOn = true, Brightness = 100 },
					new LightControlViewModel { Id = 6, Name = "Bedroom Bedside Left", IsOn = false, Brightness = 30 },
					new LightControlViewModel { Id = 7, Name = "Bedroom Bedside Right", IsOn = false, Brightness = 30 },
					new LightControlViewModel { Id = 8, Name = "Bedroom Main Overhead", IsOn = false, Brightness = 0 }
				}
			};
			return viewModel;
		}
	}
}
