using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	public class ErrorController : BaseController
	{
		// Deine the status code in the Problem(statuscode: code) to display appropriate error

		[Route("/error")]
		public IActionResult Error()
		{
			// Get Exception from HttpContext
			Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
			var (statusCode, message) = exception switch
			{
				// If the exception is a DuplicateEmailException
				DuplicateEmailException => (StatusCodes.Status409Conflict, "Email already exists."),

				// Default Error - Assigns status code 500 and standard message to the variables
				_ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred."),
			};

			return Problem(statusCode: statusCode, title: message);
		}
	}
}
