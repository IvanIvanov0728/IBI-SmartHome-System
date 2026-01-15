using IBI_SmartHome_System.Service.LightingService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	public class LightingController : Controller
	{
		private readonly ILightingService _lightingService;

		public LightingController(ILightingService lightingService)
		{
			_lightingService = lightingService;
		}

		public IActionResult Index()
		{
			var viewModel = _lightingService.GetLightingViewModel();
			return View(viewModel);
		}
	}
}
