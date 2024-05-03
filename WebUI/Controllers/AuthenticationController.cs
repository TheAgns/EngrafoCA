using Application.Authentication;
using Application.Authentication.Commands.RegisterNewUser;
using Application.Authentication.Queries.Login;
using Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("auth")]
	public class AuthenticationController : BaseController
	{
		private readonly ISender _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
		public async Task<IActionResult> Register(UserDto request)
		{
			var cmd = new RegisterNewUserCommand();
			cmd.UserDto = request;

			ErrorOr<AuthenticationResult> authRes = await _mediator.Send(cmd);

			if (authRes.IsError && authRes.FirstError == Errors.Authentication.InvalidCredentials)
			{
				return Problem(statusCode: StatusCodes.Status400BadRequest, title: authRes.FirstError.Description);
			}

			return Ok(authRes);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto request)
		{
			var qry = new LoginQuery();
			qry.LoginDto = request;

			ErrorOr<AuthenticationResult> authRes = await _mediator.Send(qry);
			
			if (authRes.IsError && authRes.FirstError == Errors.Authentication.InvalidCredentials)
			{
				return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authRes.FirstError.Description);
			}

			return Ok(authRes);
		}
	}
}
