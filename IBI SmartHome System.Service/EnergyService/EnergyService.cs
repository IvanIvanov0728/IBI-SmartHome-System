using IBI_SmartHome_System.Data;
using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.EnergyService
{
	public class EnergyService : IEnergyService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public EnergyService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

		public async Task<EnergyViewModel> GetEnergyDataAsync()
		{
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return new EnergyViewModel();

			var random = new Random();
			var weeklyData = new List<WeeklyDataPoint>();
			var hourlyData = new List<HourlyDataPoint>();
			var roomUsageData = new List<RoomUsage>();

			for (int i = 0; i < 7; i++)
			{
				weeklyData.Add(new WeeklyDataPoint { Name = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" }[i], Usage = random.Next(30, 60), Solar = random.Next(10, 25) });
			}

			for (int i = 0; i < 7; i++)
			{
				hourlyData.Add(new HourlyDataPoint { Time = $"{i * 4:00}:00", Value = random.NextDouble() * 5 });
			}

			// Get actual rooms for the user's house
			var rooms = await _context.Room
				.Where(r => r.HouseId == houseId.Value)
				.ToListAsync();

			foreach (var room in rooms)
			{
				// Assign some mock usage data for now
				roomUsageData.Add(new RoomUsage { Room = room.Name, Usage = $"{random.Next(5, 20)} kWh", Color = GetRandomColor(), Percent = random.Next(10, 40) });
			}

			// Ensure percentages sum to 100 (or close to it) if necessary, or just use as is.
			// For now, simple random percentages are fine for mock data.

			var viewModel = new EnergyViewModel
			{
				WeeklyData = weeklyData,
				HourlyData = hourlyData,
				RoomData = roomUsageData,
				EnvironmentalImpact = new EnvironmentalImpact { Co2Offset = 42.8, TreesSaved = 3 },
				BatteryStorage = new BatteryStorage { Percentage = 88, EstimatedTimeRemaining = "14h 20m" }
			};

			return viewModel;
		}

		private string GetRandomColor()
		{
			string[] colors = { "bg-blue-400", "bg-orange-400", "bg-purple-400", "bg-gray-400", "bg-green-400", "bg-red-400" };
			Random random = new Random();
			return colors[random.Next(colors.Length)];
		}
	}
}
