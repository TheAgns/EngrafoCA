using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Home()
        {
            _logger.LogInformation("Home Page Log");
            return View("Home");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
			_logger.LogInformation("Privacy Page Log");
			return View();
        }
    }
}
