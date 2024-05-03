using Application.Authentication;
using Application.Authentication.Commands.RegisterNewUser;
using Application.Authentication.Queries.Login;
using Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
	[Route("auth")]
	public class AuthenticationController : BaseController
	{
		private readonly ISender _sender;
		public AuthenticationController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterNewUserCommand command)
		{
			ErrorOr<AuthenticationResponse> authRes = await _sender.Send(command);

			if (authRes.IsError && authRes.FirstError == Errors.Authentication.InvalidCredentials)
			{
				return Problem(
					statusCode: StatusCodes.Status400BadRequest,
					title: authRes.FirstError.Description);
			}

			return authRes.Match(
				authRes => Ok(authRes),
				errors => Problem(errors));
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginQuery query)
		{
			ErrorOr<AuthenticationResponse> authRes = await _sender.Send(query);

			if (authRes.IsError && authRes.FirstError == Errors.Authentication.InvalidCredentials)
			{
				return Problem(
					statusCode: StatusCodes.Status401Unauthorized,
					title: authRes.FirstError.Description);
			}

			return authRes.Match(
				authRes => Ok(authRes),
				errors => Problem(errors));
		}
	}
}
