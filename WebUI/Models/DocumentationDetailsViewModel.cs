using Application.Features.Documentations.Queries;
using Application.Features.DocumentationTemplates.Queries;

namespace WebUI.Models
{
    public class DocumentationDetailsViewModel
    {
       public DocumentationDto Documentation { get; set; }
       public List<DocumentationTemplateHeadingDto> DocumentationTemplateHeadings { get; set; }
    }
}
