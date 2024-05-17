using Domain.Common;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.DocumentationTemplate
{
    public sealed class DocumentationTemplate : AggregateRoot<DocumentationTemplateId>
    {
        public string Title { get; private set; }

        private readonly List<DocumentationTemplateHeading> _documentationTemplateHeadings = new List<DocumentationTemplateHeading>();

        public IReadOnlyList<DocumentationTemplateHeading> DocumentationTemplateHeadings => _documentationTemplateHeadings.AsReadOnly();

        private DocumentationTemplate() { }

        public void AddHeading(DocumentationTemplateHeading heading)
        {
            _documentationTemplateHeadings.Add(heading);
        }

        public DocumentationTemplate(DocumentationTemplateId id, string title, List<DocumentationTemplateHeading> templateHeadings) : base(id)
        {
            Title = title;
            _documentationTemplateHeadings = templateHeadings;
        }

        public static DocumentationTemplate Create(string title, List<DocumentationTemplateHeading> templateHeadings)
        {
            return new(DocumentationTemplateId.CreateUnique(), title, templateHeadings);
        }
    }
}
