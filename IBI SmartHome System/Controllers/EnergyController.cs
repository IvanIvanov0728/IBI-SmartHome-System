using IBI_SmartHome_System.Service.EnergyService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EnergyController : ControllerBase
	{
		private readonly IEnergyService _energyService;

		public EnergyController(IEnergyService energyService)
		{
			_energyService = energyService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var energyData = await _energyService.GetEnergyDataAsync();
			if (energyData == null)
			{
				return NotFound();
			}
			return Ok(energyData);
		}
	}
}
