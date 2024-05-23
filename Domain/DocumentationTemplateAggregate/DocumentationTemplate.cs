using Domain.Common.Abstractions;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.DocumentationTemplate
{
    public sealed class DocumentationTemplate : AggregateRoot<DocumentationTemplateId>
    {
        public string Title { get; private set; }

        private readonly List<DocumentationTemplateHeading> _documentationTemplateHeadings = new List<DocumentationTemplateHeading>();

        public IReadOnlyList<DocumentationTemplateHeading> DocumentationTemplateHeadings => _documentationTemplateHeadings.AsReadOnly();

        public DocumentationTemplate(DocumentationTemplateId id, string title, List<DocumentationTemplateHeading> templateHeadings) : base(id)
        {
            Title = title;
            _documentationTemplateHeadings = templateHeadings;
        }

        public static DocumentationTemplate Create(string title, List<DocumentationTemplateHeading> templateHeadings)
        {
            return new(DocumentationTemplateId.CreateUnique(), title, templateHeadings);
        }

        private DocumentationTemplate() { }
    }
}
