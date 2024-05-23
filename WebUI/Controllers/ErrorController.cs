using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
	{
		[Route("Error")]
        public IActionResult Error()
		{
			if (TempData["ErrorViewModel"] != null)
			{
				var errorViewModel = System.Text.Json.JsonSerializer.Deserialize<ErrorViewModel>((string)TempData["ErrorViewModel"]);
				return View(errorViewModel);
			}

			return View(new ErrorViewModel("Unknown", "No details provided."));
		}
	}
}
