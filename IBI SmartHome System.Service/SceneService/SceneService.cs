using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IBI_SmartHome_System.Data.Entity;

namespace IBI_SmartHome_System.Service.SceneService
{
	public class SceneService : ISceneService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SceneService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

		public async Task<bool> ExecuteSceneAsync(int sceneId)
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return false;

			var scene = await _context.Scenes
				.Include(s => s.SceneActions)
				.FirstOrDefaultAsync(s => s.Id == sceneId && s.HouseId == houseId.Value);

			if (scene == null)
			{
				return false;
			}

			foreach (var action in scene.SceneActions)
			{
				var device = await _context.Device
					.Include(d => d.Lamp)
					.Include(d => d.Temperature)
					.Include(d => d.Room) // Ensure Room is included to check HouseId
					.FirstOrDefaultAsync(d => d.Id == action.DeviceId && d.Room.HouseId == houseId.Value);

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
			return true;
		}
	}
}
