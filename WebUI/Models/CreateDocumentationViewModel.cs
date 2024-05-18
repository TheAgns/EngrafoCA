using Application.Features.Documentations.Queries;
using Application.Features.DocumentationTemplates.Queries;

namespace WebUI.Models
{
    public class CreateDocumentationViewModel
    {
        public string Name { get; set; }
        public Guid DocumentationTemplateId { get; set; }
        public List<DocumentationTemplateDto> DocumentationTemplates { get; set; }
        public List<DocumentationTemplateHeadingDto> SelectedTemplateHeadings { get; set; }
        public List<DocumentationItemDto> DocumentationItems { get; set; }
    }
}
