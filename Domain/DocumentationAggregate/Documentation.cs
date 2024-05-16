using Domain.Common;
using Domain.DocumentationAggregate.Entities;
using Domain.DocumentationAggregate.ValueObjects;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.DocumentationAggregate
{
    public sealed class Documentation : AggregateRoot<DocumentationId>
    {
        // The list containing the content of the documentation
        private readonly List<DocumentationHeadingContent> _documentationHeadingContents = new List<DocumentationHeadingContent>();
        public string Name { get; private set; }

        public DocumentationCategory Category { get; private set; }

        public bool ReadOnly { get; private set; }

        public bool Hidden { get; private set; }

        public IReadOnlyList<DocumentationHeadingContent> DocumentationHeadingContents => _documentationHeadingContents.AsReadOnly();

        public DocumentationTemplateId TemplateId { get; private set; }

        private Documentation() {}

        public Documentation(DocumentationId id, string name, DocumentationTemplateId templateId, List<DocumentationHeadingContent> documentationHeadingContents, bool readOnly, bool hidden) : base(id)
        {
            Name = name;
            Category = DocumentationCategory.New();
            TemplateId = templateId;
            _documentationHeadingContents = documentationHeadingContents;
            ReadOnly = readOnly;
            Hidden = hidden;
        }

        public static Documentation Create(string name, DocumentationTemplateId templateId, List<DocumentationHeadingContent> documentationHeadingContents, bool readOnly, bool hidden)
        {
            return new(DocumentationId.CreateUnique(), name, templateId , documentationHeadingContents, readOnly, hidden);
        }
    }
}
