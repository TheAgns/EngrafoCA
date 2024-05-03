using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			ConfigureUsersTable(builder);
		}

		private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			// When implementing ValueObject remember conversion of Id. (Strongly typed)
			// builder.Property(u => u.Id).HasConversion(userId => userId.Value, value => new UserId(value));

			
		}
	}
}
