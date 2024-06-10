using Application.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace Infrastructure
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfigurationManager configuration)
		{

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("Default")));

			services.AddScoped<IApplicationDbContext>(sp =>
				sp.GetRequiredService<ApplicationDbContext>());

			services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

			services.AddScoped<PublishDomainEventsInterceptor>();

			services.AddSingleton(Log.Logger);

			return services;
		}
	}
}
