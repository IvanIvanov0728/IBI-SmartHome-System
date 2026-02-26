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

namespace IBI_SmartHome_System.Service.SecurityService
{
	public class SecurityService : ISecurityService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SecurityService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		private string GetCurrentUserId()
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}

		private async Task<int?> GetCurrentUserHouseIdAsync()
		{
			var userId = GetCurrentUserId();
			if (string.IsNullOrEmpty(userId)) return null;

			var house = await _context.Houses.FirstOrDefaultAsync(h => h.UserId == userId);
			return house?.Id;
		}

		public async Task<SecurityViewModel> GetSecurityOverviewAsync()
		{
			var userId = GetCurrentUserId();
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return new SecurityViewModel();

			var entryPoints = await _context.Device
				.Include(d => d.Room)
				.Where(d => d.HouseId == houseId.Value && (d.IsDoor || d.IsWindow))
				.Select(d => new EntryPointViewModel
				{
					DeviceId = d.Id,
					Name = d.Name,
					IsLocked = d.IsLocked,
					Type = d.IsDoor ? "Door" : (d.IsWindow ? "Window" : "Other"),
					RoomName = d.Room.Name
				})
				.ToListAsync();

			var cameras = await _context.Cameras
				.Where(c => c.HouseId == houseId.Value)
				.Select(c => new CameraViewModel
				{
					Id = c.Id,
					Name = c.Name,
					StreamUrl = c.StreamUrl,
					IsLive = c.IsLive
				})
				.ToListAsync();

			var activityLog = await _context.ActivityLogEntries
				.Where(a => a.HouseId == houseId.Value)
				.OrderByDescending(a => a.Timestamp)
				.Take(10) // Get last 10 entries for overview
				.Select(a => new ActivityLogEntryViewModel
				{
					Id = a.Id,
					Timestamp = a.Timestamp,
					Event = a.Event,
					Type = a.Type
				})
				.ToListAsync();

			// Simulate overall system status (e.g., armed if all entry points are locked)
			bool isArmed = entryPoints.All(ep => ep.IsLocked);

			return new SecurityViewModel
			{
				EntryPoints = entryPoints,
				Cameras = cameras,
				ActivityLog = activityLog,
				IsSystemArmed = isArmed,
				SystemStatusMessage = isArmed ? "Armed (Home)" : "Disarmed"
			};
		}

		public async Task<bool> UpdateEntryPointLockStatus(int deviceId, bool isLocked)
		{
			var userId = GetCurrentUserId(); // Keep userId for logging who did it
			var houseId = await GetCurrentUserHouseIdAsync();
			if (!houseId.HasValue) return false;

			var device = await _context.Device
				.FirstOrDefaultAsync(d => d.Id == deviceId && d.HouseId == houseId.Value && (d.IsDoor || d.IsWindow));

			if (device == null) return false;

			device.IsLocked = isLocked;
			await _context.SaveChangesAsync();

			await AddActivityLogEntryAsync($"{device.Name} {(isLocked ? "Locked" : "Unlocked")}", "info", deviceId);
			return true;
		}

		public async Task AddActivityLogEntryAsync(string eventDescription, string type, int? deviceId = null)
		{
			var userId = GetCurrentUserId();
			var houseId = await GetCurrentUserHouseIdAsync();
			if (string.IsNullOrEmpty(userId) || !houseId.HasValue) return;

			var entry = new ActivityLogEntry
			{
				HouseId = houseId.Value,
				Timestamp = DateTime.UtcNow,
				Event = eventDescription,
				Type = type,
				DeviceId = deviceId
			};

			_context.ActivityLogEntries.Add(entry);
			await _context.SaveChangesAsync();
		}
	}
}
