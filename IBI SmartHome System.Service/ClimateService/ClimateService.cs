using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.ClimateService
{
	public class ClimateService : IClimateService
	{
		private readonly ApplicationDbContext _context;

		public ClimateService(ApplicationDbContext context)
		{
			_context = context;
		}

		public ClimateViewModel GetClimateViewModel()
		{
			var viewModel = new ClimateViewModel
			{


				Thermostats = _context.Temperature.Select(t => new ThermostatViewModel
				{
					Id = t.Id,
					Name = t.Device.Name,
					Temperature = t.TemperatureValue,
					Humidity = t.Humidity,
					TargetTemperature = t.TargetTemperature
				}).ToList()
			};
			return viewModel;
		}

		public async Task UpdateTargetTemperature(int temperature)
		{
			var firstThermostat = await _context.Temperature.FirstOrDefaultAsync();
			if (firstThermostat != null)
			{
				firstThermostat.TargetTemperature = temperature;
				await _context.SaveChangesAsync();
			}
		}
	}
}
