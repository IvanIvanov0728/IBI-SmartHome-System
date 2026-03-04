using IBI_SmartHome_System.Models;
using IBI_SmartHome_System.Service.SecurityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class SecurityController : ControllerBase
	{
		private readonly ISecurityService _securityService;

		public SecurityController(ISecurityService securityService)
		{
			_securityService = securityService;
		}

		[HttpGet("overview")]
		public async Task<IActionResult> GetSecurityOverview()
		{
			var overview = await _securityService.GetSecurityOverviewAsync();
			return Ok(overview);
		}

		[HttpPut("entrypoint/{deviceId}/status")]
		public async Task<IActionResult> UpdateEntryPointStatus(int deviceId, [FromBody] bool isLocked)
		{
			var result = await _securityService.UpdateEntryPointLockStatus(deviceId, isLocked);
			if (!result) return BadRequest(new { message = "Failed to update entry point status or device not found/owned." });
			return Ok(new { message = "Entry point status updated successfully." });
		}

		// Example: Endpoint to manually add an activity log entry (e.g., from an IoT device or another service)
		[HttpPost("activitylog")]
		public async Task<IActionResult> AddActivityLog([FromBody] AddActivityLogRequest request)
		{
			await _securityService.AddActivityLogEntryAsync(request.EventDescription, request.Type, request.DeviceId);
			return Ok(new { message = "Activity log entry added." });
		}
	}
}
