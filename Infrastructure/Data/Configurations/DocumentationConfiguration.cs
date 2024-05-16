using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Documentation;

namespace Infrastructure.Data.Configurations
{
    public class DocumentationConfiguration : IEntityTypeConfiguration<Documentation>
    {
        public void Configure(EntityTypeBuilder<Documentation> builder)
        {
            ConfigureDocumentationsTable(builder);
        }

        private void ConfigureDocumentationsTable(EntityTypeBuilder<Documentation> builder)
        {
            builder.ToTable("Documentations");
            builder.HasKey(d => d.Id);

            builder.HasData(
                new Documentation
                {
                    Name = "Doc1"
                },
                new Documentation
                {
                    Name = "Doc2"
                },
                new Documentation
                {
                    Name = "Doc3"
                });

            // When implementing ValueObject remember conversion of Id. (Strongly typed)
            // builder.Property(u => u.Id).HasConversion(userId => userId.Value, value => new UserId(value));


        }
    }
}
