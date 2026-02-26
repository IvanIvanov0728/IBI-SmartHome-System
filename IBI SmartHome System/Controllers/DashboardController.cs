using IBI_SmartHome_System.Service.DashboardService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class DashboardController : ControllerBase
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		[HttpGet]
		public async Task<IActionResult> GetDashboard()
		{
			var dashboardViewModel = await _dashboardService.GetDashboardViewModelAsync();
			return Ok(dashboardViewModel);
		}
	}
}
