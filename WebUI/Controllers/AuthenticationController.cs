using Application.Authentication;
using Application.Authentication.Commands.RegisterNewUser;
using Application.Authentication.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("auth")]
	public class AuthenticationController : Controller
	{
		private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
		public IActionResult Register(RegisterNewUserCommand request)
		{
			var authRes = _mediator.Send(request);

			return Ok(authRes);
		}

		[HttpPost("login")]
		public IActionResult Login(LoginDto request)
		{
			var authRes = _mediator.Send(request);


			return Ok(authRes);
		}
	}
}
