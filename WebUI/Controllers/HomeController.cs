using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View("Home");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
