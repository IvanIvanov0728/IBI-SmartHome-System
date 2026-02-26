using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.ClimateService
{
	public class ClimateService : IClimateService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ClimateService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		private async Task<int?> GetCurrentUserHouseIdAsync()
		{
			var user = _httpContextAccessor.HttpContext?.User;

			if (user?.Identity?.IsAuthenticated != true)
				return null;

			// 1. Try the standard way
			// 2. Try the long URI explicitly (matching your debug output)
			// 3. Try "sub" (standard JWT/OIDC)
			var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
						 ?? user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value
						 ?? user.FindFirst("sub")?.Value;

			if (string.IsNullOrEmpty(userId)) return null;

			// Now that we have "8e445865-a24d-4543-a6c6-9443d048cdb9", this query will work
			var house = await _context.Houses.FirstOrDefaultAsync(h => h.UserId == userId);

			return house?.Id;
		}

		public async Task UpdateTargetTemperature(int targetTemperature)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return;

			var temps = await _context.Temperature
				.Include(t => t.Device).ThenInclude(d => d.Room)
				.Where(t => t.Device.Room.HouseId == houseId.Value)
				.ToListAsync();

			foreach (var temp in temps)
			{
				temp.TargetTemperature = targetTemperature;
			}
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ClimateScheduleViewModel>> GetScheduleAsync()
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return new List<ClimateScheduleViewModel>();

			return await _context.ClimateSchedules
				.Where(s => s.HouseId == houseId.Value)
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
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return new ClimateViewModel();

			var thermostats = await _context.Temperature
				.Include(t => t.Device).ThenInclude(d => d.Room)
				.Where(t => t.Device.Room.HouseId == houseId.Value)
				.Select(t => new ThermostatViewModel
				{
					Humidity = t.Humidity,
					TargetTemperature = t.TargetTemperature,
					Temperature = t.TemperatureValue,
				})
				.ToListAsync();

			var firstThermostat = thermostats.FirstOrDefault();

			return new ClimateViewModel
			{
				CurrentTemperature = firstThermostat?.Temperature ?? 0,
				TargetTemperature = firstThermostat?.TargetTemperature ?? 22,
				Thermostats = thermostats
			};
		}

		public async Task<ClimateScheduleViewModel> AddScheduleEntryAsync(ClimateScheduleViewModel newEntry)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return newEntry;

			var schedule = new ClimateSchedule
			{
				Day = newEntry.Day,
				Time = newEntry.Time,
				Temp = newEntry.Temp,
				Mode = newEntry.Mode,
				HouseId = houseId.Value
			};

			_context.ClimateSchedules.Add(schedule);
			await _context.SaveChangesAsync();

			newEntry.Id = schedule.Id;
			return newEntry;
		}

		public async Task<bool> UpdateScheduleEntryAsync(int id, ClimateScheduleViewModel updatedEntry)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return false;

			var schedule = await _context.ClimateSchedules
				.FirstOrDefaultAsync(s => s.Id == id && s.HouseId == houseId.Value);

			if (schedule == null)
			{
				return false;
			}

			schedule.Day = updatedEntry.Day;
			schedule.Time = updatedEntry.Time;
			schedule.Temp = updatedEntry.Temp;
			schedule.Mode = updatedEntry.Mode;

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteScheduleEntryAsync(int id)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return false;

			var schedule = await _context.ClimateSchedules
				.FirstOrDefaultAsync(s => s.Id == id && s.HouseId == houseId.Value);

			if (schedule == null)
			{
				return false;
			}

			_context.ClimateSchedules.Remove(schedule);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
