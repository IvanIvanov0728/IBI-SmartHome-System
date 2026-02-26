using IBI_SmartHome_System.Service.Models;
using IBI_SmartHome_System.Service.SettingsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize] // All settings endpoints require authentication
	public class SettingsController : ControllerBase
	{
		private readonly ISettingsService _settingsService;

		public SettingsController(ISettingsService settingsService)
		{
			_settingsService = settingsService;
		}

		[HttpGet("profile")]
		public async Task<IActionResult> GetUserProfile()
		{
			var profile = await _settingsService.GetUserProfileAsync();
			if (profile == null) return NotFound();
			return Ok(profile);
		}

		[HttpPut("profile")]
		public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileViewModel model)
		{
			var result = await _settingsService.UpdateUserProfileAsync(model);
			if (!result) return BadRequest(new { message = "Failed to update user profile." });
			return Ok(new { message = "User profile updated successfully." });
		}

		[HttpGet("user-settings")]
		public async Task<IActionResult> GetUserSettings()
		{
			var settings = await _settingsService.GetUserSettingsAsync();
			if (settings == null) return NotFound();
			return Ok(settings);
		}

		[HttpPut("user-settings")]
		public async Task<IActionResult> UpdateUserSettings([FromBody] UserSettingsViewModel model)
		{
			var result = await _settingsService.UpdateUserSettingsAsync(model);
			if (!result) return BadRequest(new { message = "Failed to update user settings." });
			return Ok(new { message = "User settings updated successfully." });
		}
	}
}
