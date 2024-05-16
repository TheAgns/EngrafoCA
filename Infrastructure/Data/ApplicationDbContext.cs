using System.Reflection;
using Application.Common.Interfaces;
using Domain.DocumentationAggregate;
using Domain.DocumentationTemplate;
using Domain.DocumentationTemplate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}


		public DbSet<Documentation> Documentations { get; set; }
    }
}
