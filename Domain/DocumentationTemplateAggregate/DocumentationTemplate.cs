using Domain.Common;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.DocumentationTemplate
{
    public sealed class DocumentationTemplate : AggregateRoot<DocumentationTemplateId>
    {
        public string Title { get; private set; }

        // Entity or ValueObject?
        private readonly List<DocumentationTemplateHeading> _templateHeadings = new();

        public IReadOnlyList<DocumentationTemplateHeading> TemplateHeadings => _templateHeadings.ToList();

        private DocumentationTemplate(DocumentationTemplateId id, string title, List<DocumentationTemplateHeading> templateHeadings) : base(id)
        {
            Title = title;
            _templateHeadings = templateHeadings;
        }

        public static DocumentationTemplate Create(string title, List<DocumentationTemplateHeading> templateHeadings)
        {
            return new(DocumentationTemplateId.CreateUnique(), title, templateHeadings);
        }
    }
}
