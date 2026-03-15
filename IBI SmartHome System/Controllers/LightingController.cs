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
		public async Task<IActionResult> GetLights()
		{
			var viewModel = await _lightingService.GetLightingViewModel();
			if (viewModel == null)
			{
				return NotFound();
			}
			return Ok(viewModel);
		}

		[HttpPut("state/{lampid}")]
		public async Task<IActionResult> UpdateLightState(int lampid, [FromBody] bool isOn)
		{
			var result = await _lightingService.UpdateLightState(lampid, isOn);
			if (!result)
				return NotFound();
			return NoContent();
		}

		[HttpPut("brightness/{lampid}")]
		public async Task<IActionResult> UpdateLightBrightness(int lampid, [FromBody] int brightness)
		{
			Console.WriteLine($"LampId: {lampid} | Brightness: {brightness}");

			var result = await _lightingService.UpdateLightBrightness(lampid, brightness);

			if (!result)
				return NotFound();

			return NoContent();
		}
	}
}
