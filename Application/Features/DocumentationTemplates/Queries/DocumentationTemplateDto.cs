using System;
using Application.Common;

namespace Application.Features.DocumentationTemplates.Queries
{
    public class DocumentationTemplateDto : BaseDto
    {
        public string Title { get; init; }
        
        public List<DocumentationTemplateHeadingDto> DocumentationTemplateHeadings { get; init; }

    }
}
