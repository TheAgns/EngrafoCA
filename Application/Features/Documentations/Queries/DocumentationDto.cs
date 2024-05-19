using Application.Common.Abstractions;

namespace Application.Features.Documentations.Queries
{
    public class DocumentationDto : BaseDto
    {
        public string Name { get; init; }
        public List<DocumentationItemDto> DocumentationItems { get; set; }

        public Guid DocumentationTemplateId { get; init; }

        public bool ReadOnly { get; init; }
        public bool Hidden { get; init; }
    }
}
