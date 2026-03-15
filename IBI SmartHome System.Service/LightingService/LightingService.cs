using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IBI_SmartHome_System.Data.Entity;

namespace IBI_SmartHome_System.Service.LightingService
{
	public class LightingService : ILightingService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LightingService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

		public async Task<LightingViewModel> GetLightingViewModel()
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return new LightingViewModel();

			var rooms = await _context.Room
				.Where(r => r.HouseId == houseId.Value)
				.Select(r => new RoomViewModel
				{
					Id = r.Id,
					Name = r.Name,
					Floor = r.Floor
				})
				.ToListAsync();

			var lights = await _context.Lamps
				.Include(l => l.Device)
				.Where(l => l.Device.Room.HouseId == houseId.Value)
				.Select(l => new LightControlViewModel
				{
					Id = l.Id,
					Name = l.Device.Name,
					IsOn = l.IsOn,
					Brightness = l.Brightness,
					RoomId = l.Device.RoomId
				}).ToListAsync();

			var viewModel = new LightingViewModel
			{
				Rooms = rooms,
				Lights = lights
			};
			return viewModel;
		}

		public async Task<bool> UpdateLightState(int lightId, bool isOn)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return false;

			var light = await _context.Lamps
				.Include(l => l.Device).ThenInclude(d => d.Room)
				.FirstOrDefaultAsync(l => l.Id == lightId && l.Device.Room.HouseId == houseId.Value);

			if (light == null)
				return false;
			light.IsOn = isOn;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateLightBrightness(int lightId, int brightness)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return false;

			var light = await _context.Lamps
				.Include(l => l.Device).ThenInclude(d => d.Room)
				.FirstOrDefaultAsync(l => l.Id == lightId && l.Device.Room.HouseId == houseId.Value);

			if (light == null)
				return false;
			light.Brightness = brightness;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
