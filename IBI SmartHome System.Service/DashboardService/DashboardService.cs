using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Models;
using IBI_SmartHome_System.Service.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.DashboardService
{
	public class DashboardService : IDashboardService
	{
		private readonly WeatherService _weatherService;
		private readonly ApplicationDbContext _context;

		public DashboardService(WeatherService weatherService, ApplicationDbContext context)
		{
			_weatherService = weatherService;
			_context = context;
		}

		public async Task<DashboardViewModel> GetDashboardViewModelAsync()
		{
			List<LightControlViewModel> lights = new List<LightControlViewModel>
			{
				new LightControlViewModel { Id = 1, Name = "Living Room Main Chandelier", IsOn = true, Brightness = 80 },
				new LightControlViewModel { Id = 2, Name = "Living Room Floor Lamp", IsOn = true, Brightness = 40 },
				new LightControlViewModel { Id = 3, Name = "Living Room Cove Lights", IsOn = true, Brightness = 60 },
				new LightControlViewModel { Id = 4, Name = "Kitchen Island Pendants", IsOn = false, Brightness = 100 },
				new LightControlViewModel { Id = 5, Name = "Kitchen Under Cabinet", IsOn = true, Brightness = 100 },
				new LightControlViewModel { Id = 6, Name = "Bedroom Bedside Left", IsOn = false, Brightness = 30 },
				new LightControlViewModel { Id = 7, Name = "Bedroom Bedside Right", IsOn = false, Brightness = 30 },
				new LightControlViewModel { Id = 8, Name = "Bedroom Main Overhead", IsOn = false, Brightness = 0 }
			};

			var dashboardViewModel = new DashboardViewModel
			{
				Lights = lights,
				WeatherOutside = await _weatherService.GetWeatherAsync(),
				TargetTemperature = 28, // add to the db for target temperature and use
									   // _context.Temperature.Select(t => t.TargetTemperature).FirstOrDefault()

				CurrentTemperature = _context.Temperature.Select(t => t.TemperatureValue).FirstOrDefault()
			};

			return dashboardViewModel;
		}

		public ThermostatViewModel GetThermostatViewModel()
		{
			return _context.Temperature.Select(t => new ThermostatViewModel
			{
				Id = t.Id,
				Name = t.Device.Name,
				Temperature = t.TemperatureValue,
				Humidity = t.Humidity
			}).FirstOrDefault();
		}
	}
}
