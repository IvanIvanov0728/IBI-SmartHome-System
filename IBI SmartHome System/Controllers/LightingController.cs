using IBI_SmartHome_System.Service.LightingService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LightingController : ControllerBase
	{
		private readonly ILightingService _lightingService;
		private readonly ILogger<LightingController> _logger;

		public LightingController(ILightingService lightingService, ILogger<LightingController> logger)
		{
			_lightingService = lightingService;
			_logger = logger;
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
			_logger.LogInformation("Updating LampId: {LampId} to Brightness: {Brightness}", lampid, brightness);

			var result = await _lightingService.UpdateLightBrightness(lampid, brightness);

			if (!result)
				return NotFound();

			return NoContent();
		}
	}
}
