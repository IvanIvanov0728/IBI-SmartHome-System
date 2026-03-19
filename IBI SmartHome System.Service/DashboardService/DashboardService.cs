using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Models;
using IBI_SmartHome_System.Service.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IBI_SmartHome_System.Data.Entity;

namespace IBI_SmartHome_System.Service.DashboardService
{
	public class DashboardService : IDashboardService
	{
		private readonly WeatherService _weatherService;
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public DashboardService(WeatherService weatherService, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_weatherService = weatherService;
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

		public async Task<DashboardViewModel> GetDashboardViewModelAsync()
		{
			var houseId = await GetCurrentUserHouseIdAsync();

			// If houseId is null here, the UserId in the 'Houses' table 
			// does not match '8e445865-a24d-4543-a6c6-9443d048cdb9'
			if (!houseId.HasValue) return new DashboardViewModel { Rooms = new List<RoomViewModel>() };

			var house = await _context.Houses.FindAsync(houseId.Value);
			double lat = house?.Latitude ?? 42.70;
			double lon = house?.Longitude ?? 23.32;

			var lights = await _context.Lamps
				.Include(l => l.Device)
				.Where(l => l.Device.Room.HouseId == houseId.Value)
				.Select(l => new LightControlViewModel
				{
					Id = l.Id,
					DeviceId = l.DeviceId,
					Name = l.Device.Name,
					IsOn = l.IsOn,
					Brightness = l.Brightness,
					RoomId = l.Device.RoomId
				}).ToListAsync();

			var rooms = await _context.Room
				.Where(r => r.HouseId == houseId.Value)
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
				.ToListAsync();

			var tempEntity = await _context.Temperature
			.Include(t => t.Device)
			.ThenInclude(d => d.Room)
			.FirstOrDefaultAsync(t => t.Device.Room.HouseId == houseId.Value);

			var scenes = await _context.Scenes
				.Where(s => s.HouseId == houseId.Value)
				.Select(s => new SceneViewModel
				{
					Id = s.Id,
					Name = s.Name,
					IsActive = false
				}).ToListAsync();

			return new DashboardViewModel
			{
				Lights = lights,
				WeatherOutside = await _weatherService.GetWeatherAsync(lat, lon),
				Rooms = rooms,
				Scenes = scenes,
				TargetTemperature = tempEntity?.TargetTemperature ?? 21, // Fallback value
				CurrentTemperature = tempEntity?.TemperatureValue ?? 20.0  // Fallback value
			};
		}

		public async Task<ThermostatViewModel> GetThermostatViewModel()
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return null;

			return await _context.Temperature
				.Where(t => t.Device.Room.HouseId == houseId.Value)
				.Select(t => new ThermostatViewModel
				{
					Id = t.Id,
					Name = t.Device.Name,
					Temperature = t.TemperatureValue,
					Humidity = t.Humidity,
					TargetTemperature = t.TargetTemperature
				}).FirstOrDefaultAsync();
		}
	}
}
