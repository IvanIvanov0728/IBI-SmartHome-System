using IBI_SmartHome_System.Data.Entity;
using IBI_SmartHome_System.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Service.SettingsService
{
	public class SettingsService : ISettingsService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SettingsService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}

		private string GetCurrentUserId()
		{
			return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}

		private async Task<ApplicationUser> GetCurrentUserAsync()
		{
			var userId = GetCurrentUserId();
			if (string.IsNullOrEmpty(userId)) return null;
			return await _userManager.FindByIdAsync(userId);
		}

		public async Task<UserProfileViewModel> GetUserProfileAsync()
		{
			var user = await GetCurrentUserAsync();
			if (user == null) return null;

			return new UserProfileViewModel
			{
				Email = user.Email,
				UserName = user.UserName,
				UserRole = user.UserRole // Custom property from ApplicationUser
			};
		}

		public async Task<bool> UpdateUserProfileAsync(UserProfileViewModel model)
		{
			var user = await GetCurrentUserAsync();
			if (user == null) return false;

			user.Email = model.Email;
			user.UserName = model.UserName; // Update username if allowed

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}

		public async Task<UserSettingsViewModel> GetUserSettingsAsync()
		{
			var user = await GetCurrentUserAsync();
			if (user == null) return null;

			return new UserSettingsViewModel
			{
				ReceiveNotifications = user.ReceiveNotifications,
				DarkModeEnabled = user.DarkModeEnabled,
			};
		}

		public async Task<bool> UpdateUserSettingsAsync(UserSettingsViewModel model)
		{
			var user = await GetCurrentUserAsync();
			if (user == null) return false;

			user.ReceiveNotifications = model.ReceiveNotifications;
			user.DarkModeEnabled = model.DarkModeEnabled;

			var result = await _userManager.UpdateAsync(user);
			return result.Succeeded;
		}
	}
}
