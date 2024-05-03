using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Authentication.Commands.RegisterNewUser;
using ErrorOr;
using MediatR;

namespace Application.Common.Behaviors
{
	public class ValidateRegisterNewUserCommandBehavior : IPipelineBehavior<RegisterNewUserCommand, ErrorOr<AuthenticationResponse>>
	{
		public Task<ErrorOr<AuthenticationResponse>> Handle(RegisterNewUserCommand request, RequestHandlerDelegate<ErrorOr<AuthenticationResponse>> next, CancellationToken cancellationToken)
		{

			// Before reaching the handler
			var result = next();
			// After reaching the handler

			return result;
		}
	}
}
