using Application.Common.Abstractions;

namespace Application.Features.Documentations.Queries
{
    public class DocumentationItemDto : BaseDto
    {
        public string Content { get; set; }

        public int Position { get; set; }
    }
}
