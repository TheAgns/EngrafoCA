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
            _logger.LogInformation("LOG TEST");
            return View("Home");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
