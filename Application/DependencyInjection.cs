using System.Reflection;
using Application.Authentication;
using Application.Authentication.Commands.RegisterNewUser;
using Application.Common.Behaviors;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMapster();
			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddScoped<IPipelineBehavior<RegisterNewUserCommand, 
				ErrorOr<AuthenticationResponse>>, 
				ValidateRegisterNewUserCommandBehavior>();
			return services;
		}
	}
}
