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
		public async Task<IActionResult> GetClimateStatus()
		{
			var viewModel = await _climateService.GetClimateViewModelAsync();
			if (viewModel == null)
			{
				return NotFound();
			}
			return Ok(viewModel);
		}

		[HttpPut("temperature")]
		public async Task<IActionResult> UpdateTemperature([FromBody] UpdateTemperatureRequest request)
		{
			if (request == null)
			{
				return BadRequest("Invalid request.");
			}
			await _climateService.UpdateTargetTemperature(request.TargetTemperature);
			return Ok();
		}

		[HttpGet("schedule")]
		public async Task<IActionResult> GetSchedule()
		{
			var schedule = await _climateService.GetScheduleAsync();
			if (schedule == null)
			{
				return NotFound();
			}
			return Ok(schedule);
		}

		[HttpPost("schedule")]
		public async Task<IActionResult> AddScheduleEntry([FromBody] Service.Models.ClimateScheduleViewModel newEntry)
		{
			if (newEntry == null)
			{
				return BadRequest("Invalid entry.");
			}
			var addedEntry = await _climateService.AddScheduleEntryAsync(newEntry);
			if (addedEntry == null)
			{
				return BadRequest("Could not add entry.");
			}
			return CreatedAtAction(nameof(GetSchedule), new { id = addedEntry.Id }, addedEntry);
		}

		[HttpPut("schedule/{id}")]
		public async Task<IActionResult> UpdateScheduleEntry(int id, [FromBody] Service.Models.ClimateScheduleViewModel updatedEntry)
		{
			var result = await _climateService.UpdateScheduleEntryAsync(id, updatedEntry);
			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpDelete("schedule/{id}")]
		public async Task<IActionResult> DeleteScheduleEntry(int id)
		{
			var result = await _climateService.DeleteScheduleEntryAsync(id);
			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
