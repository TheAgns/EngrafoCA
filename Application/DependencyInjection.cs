using System.Reflection;
using Application.Common.Behaviors;
using FluentValidation;
using Mapster;
using MediatR;
using Application.Common.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Mapping
			services.AddMapster();

			// Adds the Mapping Configurations
			services.AddMappings();

			// MediatR
			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			// Validation Interceptor using MediatR pipeline
			services.AddScoped(
				typeof(IPipelineBehavior<,>), 
				typeof(ValidationBehavior<,>));

			// Logging Interceptor using MediatR pipeline
			services.AddScoped(
				typeof(IPipelineBehavior<,>),
				typeof(RequestLogPipelineBehavior<,>));

			// FluentValidation
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			

            return services;
		}
	}
}
