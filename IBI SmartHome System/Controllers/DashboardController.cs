using IBI_SmartHome_System.Service.DashboardService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		public async Task<IActionResult> Index()
		{
			var dashboardViewModel = await _dashboardService.GetDashboardViewModelAsync();
			ViewBag.Thermostat = _dashboardService.GetThermostatViewModel();
			return View(dashboardViewModel);
		}
	}
}
