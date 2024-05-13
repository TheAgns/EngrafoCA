using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
	{
        public IActionResult Error()
		{	
			return View();
		}
	}
}
