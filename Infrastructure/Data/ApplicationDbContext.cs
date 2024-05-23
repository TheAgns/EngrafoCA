using System.Reflection;
using Application.Common.Interfaces;
using Domain.Common.Abstractions;
using Domain.DocumentationAggregate;
using Domain.DocumentationTemplate;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, PublishDomainEventsInterceptor domainEventsInterceptor)
			: base(options) 
		{ 
			_publishDomainEventsInterceptor = domainEventsInterceptor;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder
				.Ignore<List<IDomainEvent>>()
				.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
			base.OnConfiguring(optionsBuilder);
		}


		public DbSet<Documentation> Documentations { get; set; }
		public DbSet<DocumentationTemplate> DocumentationTemplates { get; set; }
    }
}
