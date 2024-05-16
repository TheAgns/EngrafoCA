using Application.Common;

namespace Application.Features.Documentations.Queries
{
    public class DocumentationDto : BaseDto
    {
        public string Name { get; set; }
        public List<DocumentationHeadingContentDto> DocumentationHeadingContents { get; set; }

        public string DocumentationTemplateId { get; set; }

        public DateTime? Created {  get; set; }

        public DateTimeOffset? LastModified { get; set; }

    }
}
