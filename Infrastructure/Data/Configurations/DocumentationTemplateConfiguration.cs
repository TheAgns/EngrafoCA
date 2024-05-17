using Domain.DocumentationTemplate;
using Domain.DocumentationTemplate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DocumentationTemplateConfiguration : IEntityTypeConfiguration<DocumentationTemplate>
    {
        public void Configure(EntityTypeBuilder<DocumentationTemplate> builder)
        {
            ConfigureDocumentationTemplatesTable(builder);
            SeedDb(builder);
        }

        private void ConfigureDocumentationTemplatesTable(EntityTypeBuilder<DocumentationTemplate> builder)
        {
            builder.ToTable("DocumentationTemplates");

            builder.HasKey(t => t.Id);

            //! Defines how the id is saved and retrieved
            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => DocumentationTemplateId.New(value));

            builder.Property(t => t.Title).HasMaxLength(50);

            builder.Metadata.FindNavigation(nameof(DocumentationTemplate.DocumentationTemplateHeadings))!.SetPropertyAccessMode(PropertyAccessMode.Field);


            builder.OwnsMany(t => t.DocumentationTemplateHeadings, th =>
            {
                th.ToTable("DocumentationTemplateHeadings");

                th.WithOwner().HasForeignKey("DocumentationTemplateId");

                th.Property<int>("Id").ValueGeneratedOnAdd();

                th.Property(th => th.Title).IsRequired();
                th.Property(th => th.Position).IsRequired();

                th.HasKey("Id");
            });
        }

        private void SeedDb(EntityTypeBuilder<DocumentationTemplate> builder)
        {
            //! Seeding the database          
            var template = DocumentationTemplate.Create(
                    "Template1",
                    new List<DocumentationTemplateHeading>()
                );

            var templateSeed = new DocumentationTemplate(template.Id, template.Title, template.DocumentationTemplateHeadings.ToList());

            builder.HasData(templateSeed);

            builder.OwnsMany(t => t.DocumentationTemplateHeadings)
                .HasData(
                new
                {
                    DocumentationTemplateId = template.Id,
                    Id = 1,
                    Title = "Heading1",
                    Position = 1,
                },
                new
                {
                    DocumentationTemplateId = template.Id,
                    Id = 2,
                    Title = "Heading2",
                    Position = 2,
                });

        }
    }
}
