using Application.Common;

namespace Application.Features.Documentations.Queries
{
    public class DocumentationHeadingContentDto : BaseDto
    {
        public string ContentType { get; set; }

        public object? Content { get; set; }

        public int Position { get; set; }
    }
}
