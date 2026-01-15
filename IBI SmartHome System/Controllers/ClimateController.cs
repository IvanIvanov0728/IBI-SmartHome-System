using IBI_SmartHome_System.Service.ClimateService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	public class ClimateController : Controller
	{
		private readonly IClimateService _climateService;

		public ClimateController(IClimateService climateService)
		{
			_climateService = climateService;
		}

		public IActionResult Index()
		{
			var viewModel = _climateService.GetClimateViewModel();
			return View(viewModel);
		}
	}
}
