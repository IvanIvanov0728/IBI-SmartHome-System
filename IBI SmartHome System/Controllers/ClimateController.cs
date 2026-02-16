using IBI_SmartHome_System.Service.ClimateService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClimateController : ControllerBase
	{
		private readonly IClimateService _climateService;

		public ClimateController(IClimateService climateService)
		{
			_climateService = climateService;
		}

		[HttpGet]
		public IActionResult GetClimateStatus()
		{
			var viewModel = _climateService.GetClimateViewModel();
			return Ok(viewModel);
		}

		[HttpPut("temperature")]
		public async Task<IActionResult> UpdateTemperature([FromBody] UpdateTemperatureRequest request)
		{
			await _climateService.UpdateTargetTemperature(request.TargetTemperature);
			return Ok();
		}
	}
}
