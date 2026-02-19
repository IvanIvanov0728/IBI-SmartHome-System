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
			List<LightControlViewModel> lights = _context.Lamps
				.Select(l => new LightControlViewModel
				{
					Id = l.Id,
					DeviceId = l.DeviceId,
					Name = l.Device.Room.Name,
					IsOn = l.IsOn,
					Brightness = l.Brightness,
					RoomId = l.Device.RoomId
				}).ToList();

			var rooms = _context.Room
				.Select(r => new RoomViewModel
				{
					Id = r.Id,
					Name = r.Name,
					Floor = r.Floor,
					Devices = r.Devices.Select(d => new DeviceViewModel
					{
						Id = d.Id,
						Name = d.Name,
						Type = d.Type.ToString()
					})
				})
				.ToList();

			var dashboardViewModel = new DashboardViewModel
			{
				Lights = lights,
				WeatherOutside = await _weatherService.GetWeatherAsync(),
				TargetTemperature = _context.Temperature.Select(t => t.TargetTemperature).FirstOrDefault(),
				CurrentTemperature = _context.Temperature.Select(t => t.TemperatureValue).FirstOrDefault(),
				Rooms = rooms
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
				Humidity = t.Humidity,
				TargetTemperature = t.TargetTemperature
			}).FirstOrDefault();
		}
	}
}
