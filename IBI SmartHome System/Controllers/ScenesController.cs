using IBI_SmartHome_System.Service.SceneService;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ScenesController : ControllerBase
	{
		private readonly ISceneService _sceneService;

		public ScenesController(ISceneService sceneService)
		{
			_sceneService = sceneService;
		}

		[HttpGet]
		public async Task<IActionResult> GetScenes()
		{
			var scenes = await _sceneService.GetScenesAsync();
			return Ok(scenes);
		}

		[HttpPost("execute/{id}")]
		public async Task<IActionResult> Execute(int id)
		{
			bool result = await _sceneService.ExecuteSceneAsync(id);
			if (!result)
			{
				return NotFound();
			}
			return Ok();
		}
	}
}
