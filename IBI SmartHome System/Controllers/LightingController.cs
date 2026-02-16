using IBI_SmartHome_System.Service.LightingService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LightingController : ControllerBase
	{
		private readonly ILightingService _lightingService;

		public LightingController(ILightingService lightingService)
		{
			_lightingService = lightingService;
		}

		[HttpGet]
		public IActionResult GetLights()
		{
			var viewModel = _lightingService.GetLightingViewModel();
			return Ok(viewModel);
		}

		[HttpPut("state/{lampid}")]
		public IActionResult UpdateLightState(int lampid, [FromBody] bool isOn)
		{
			var result = _lightingService.UpdateLightState(lampid, isOn);
			if (!result)
				return NotFound();
			return NoContent();
		}

		[HttpPut("brightness/{lampid}")]
		public IActionResult UpdateLightBrightness(int lampid, [FromBody] int brightness)
		{
			Console.WriteLine($"LampId: {lampid} | Brightness: {brightness}");

			var result = _lightingService.UpdateLightBrightness(lampid, brightness);

			if (!result)
				return NotFound();

			return NoContent();

			//var result = _lightingService.UpdateLightBrightness(lampid, brightness);
			//if (!result)
			//	return NotFound();
			//return NoContent();
		}
	}
}
