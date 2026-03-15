using IBI_SmartHome_System.Models;
using IBI_SmartHome_System.Service.AdminService;
using IBI_SmartHome_System.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[Authorize(Roles = "Admin")]
	[ApiController]
	[Route("api/[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
		{
			_adminService = adminService;
		}

		[HttpGet("users/search")]
		public async Task<IActionResult> SearchUsers([FromQuery] string q)
		{
			var results = await _adminService.SearchUsersAsync(q);
			return Ok(results);
		}

		[HttpPost("houses")]
		public async Task<IActionResult> CreateHouse([FromBody] CreateHouseViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var id = await _adminService.CreateHouseAsync(model);
			return Ok(new { id, message = "House created successfully" });
		}

		[HttpPost("rooms")]
		public async Task<IActionResult> AddRoom([FromBody] CreateRoomViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var id = await _adminService.AddRoomToHouseAsync(model);
			return Ok(new { id, message = "Room added successfully" });
		}

		[HttpPost("devices")]
		public async Task<IActionResult> AddDevice([FromBody] CreateDeviceViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			try
			{
				var id = await _adminService.AddDeviceToRoomAsync(model);
				return Ok(new { id, message = "Device added successfully" });
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

		[HttpGet("hierarchy")]
		public async Task<IActionResult> GetHierarchy()
		{
			var hierarchy = await _adminService.GetHousesWithHierarchyAsync();
			if (hierarchy == null)
			{
				return NotFound();
			}
			return Ok(hierarchy);
		}

		[HttpGet("logs")]
		public async Task<IActionResult> GetLogs()
		{
			var logs = await _adminService.GetGlobalActivityLogsAsync();
			return Ok(logs);
		}

		[HttpGet("analytics")]
		public async Task<IActionResult> GetAnalytics()
		{
			var analytics = await _adminService.GetGlobalAnalyticsAsync();
			return Ok(analytics);
		}

		[HttpGet("rules")]
		public async Task<IActionResult> GetRules()
		{
			var rules = await _adminService.GetAutomationRulesAsync();
			return Ok(rules);
		}

		[HttpPost("rules")]
		public async Task<IActionResult> CreateRule([FromBody] CreateAutomationRuleViewModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var id = await _adminService.CreateAutomationRuleAsync(model);
			return Ok(new { id });
		}

		[HttpDelete("rules/{id}")]
		public async Task<IActionResult> DeleteRule(int id)
		{
			await _adminService.DeleteAutomationRuleAsync(id);
			return Ok();
		}
	}
}
