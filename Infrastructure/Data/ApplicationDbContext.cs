using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)	{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		//public override Task<Int> SaveChangesAsync(CancellationToken cancellationToken = default)
		//{
		//	foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		//	{
		//		entry.Entity.LastModifiedDate = DateTime.Now;

		//		if(entry.State == EntityState.Added)
		//		{
		//			entry.Entity.CreatedDate = DateTime.Now;
		//		}
		//	}

		//	return base.SaveChangesAsync(cancellationToken);
		//}

		public DbSet<Documentation> Documentations { get; set; }
		public DbSet<User> Users { get; set; }


	}
}
