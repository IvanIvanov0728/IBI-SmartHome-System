using System.Diagnostics;
using IBI_SmartHome_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace IBI_SmartHome_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult Scenes()
		{
			return View();
		}

		public IActionResult Security()
		{
			return View();
		}

		public IActionResult Settings()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
