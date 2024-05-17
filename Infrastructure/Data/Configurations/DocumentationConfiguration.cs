using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.DocumentationAggregate;
using Domain.DocumentationAggregate.ValueObjects;
using Domain.DocumentationTemplate.ValueObjects;
using Domain.DocumentationAggregate.Entities;

namespace Infrastructure.Data.Configurations
{
    public class DocumentationConfiguration : IEntityTypeConfiguration<Documentation>
    {
        public void Configure(EntityTypeBuilder<Documentation> builder)
        {
            ConfigureDocumentationsTable(builder);
            ConfigureDocumentationItemsTable(builder);
        }

        private void ConfigureDocumentationsTable(EntityTypeBuilder<Documentation> builder)
        {
            builder.ToTable("Documentations");
            builder.HasKey(d => d.Id);

            //! Defines how the id is saved and retrieved
            builder.Property(d => d.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => DocumentationId.New(value));

            builder.Property(d => d.Name).HasMaxLength(50);

            builder.OwnsOne(d => d.Category);

            builder.Property(d => d.TemplateId)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => DocumentationTemplateId.New(value));

            //! Populates the public ReadOnlyList<DocumentationItem>
            builder.Metadata.FindNavigation(nameof(Documentation.DocumentationItems))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureDocumentationItemsTable(EntityTypeBuilder<Documentation> builder)
        {
            builder.OwnsMany(d => d.DocumentationItems, di =>
            {
                di.ToTable("DocumentationItems");

                di.WithOwner().HasForeignKey("DocumentationId");

                //! Sets the composite key for the entity inside the aggregate
                di.HasKey(nameof(DocumentationItem.Id), "DocumentationId");

                di.Property(di => di.Id)
                    .HasColumnName("DocumentationItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id.Value,
                    value => DocumentationItemId.New(value));

                di.Property(dhc => dhc.Content).IsRequired();
                di.Property(dhc => dhc.Position).IsRequired();
            });
        }
    }
}
