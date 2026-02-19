using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
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

		public async Task UpdateTargetTemperature(int targetTemperature)
		{
			var temps = await _context.Temperature.ToListAsync();
			foreach (var temp in temps)
			{
				temp.TargetTemperature = targetTemperature;
			}

			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ClimateScheduleViewModel>> GetScheduleAsync()
		{
			return await _context.ClimateSchedules
				.Select(s => new ClimateScheduleViewModel
				{
					Id = s.Id,
					Day = s.Day,
					Time = s.Time,
					Temp = s.Temp,
					Mode = s.Mode
				})
				.ToListAsync();
		}

		public async Task<ClimateViewModel> GetClimateViewModelAsync()
		{
			var thermostats = await _context.Temperature
				.Include(t => t.Device)
				.Select(t => new ThermostatViewModel
				{
					Humidity = t.Humidity,
					TargetTemperature = t.TargetTemperature
				})
				.ToListAsync();

			var firstThermostat = thermostats.FirstOrDefault();

			return new ClimateViewModel
			{
				CurrentTemperature = firstThermostat?.Temperature ?? 0,
				TargetTemperature = firstThermostat?.TargetTemperature ?? 0,
				Thermostats = thermostats
			};
		}

		public async Task<ClimateScheduleViewModel> AddScheduleEntryAsync(ClimateScheduleViewModel newEntry)
		{
			var schedule = new ClimateSchedule
			{
				Day = newEntry.Day,
				Time = newEntry.Time,
				Temp = newEntry.Temp,
				Mode = newEntry.Mode
			};

			_context.ClimateSchedules.Add(schedule);
			await _context.SaveChangesAsync();

			newEntry.Id = schedule.Id;
			return newEntry;
		}



	}
}
