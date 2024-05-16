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
            ConfigureDocumentationHeadingContentsTable(builder);
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

            //! Populates the public ReadOnlyList<DocumentationHeadingContent>
            builder.Metadata.FindNavigation(nameof(Documentation.DocumentationHeadingContents))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureDocumentationHeadingContentsTable(EntityTypeBuilder<Documentation> builder)
        {
            builder.OwnsMany(d => d.DocumentationHeadingContents, dhc =>
            {
                dhc.ToTable("DocumentationHeadingContents");

                dhc.WithOwner().HasForeignKey("DocumentationId");

                //! Sets the composite key for the entity inside the aggregate
                dhc.HasKey(nameof(DocumentationHeadingContent.Id), "DocumentationId");

                dhc.Property(dhc => dhc.Id)
                    .HasColumnName("DocumentationHeadingContentId")
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id.Value,
                    value => DocumentationHeadingContentId.New(value));

                dhc.Property(dhc => dhc.Content).IsRequired();
                dhc.Property(dhc => dhc.Position).IsRequired();
                dhc.Property(dhc => dhc.ContentType).IsRequired();
            });
        }
    }
}
