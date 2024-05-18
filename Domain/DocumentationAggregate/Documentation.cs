using Domain.Common;
using Domain.DocumentationAggregate.Entities;
using Domain.DocumentationAggregate.ValueObjects;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.DocumentationAggregate
{
    public sealed class Documentation : AggregateRoot<DocumentationId>
    {
        // The list containing the content of the documentation
        private readonly List<DocumentationItem> _documentationItems = new List<DocumentationItem>();
        public string Name { get; private set; }

        public DocumentationCategory Category { get; private set; }

        public bool ReadOnly { get; private set; }

        public bool Hidden { get; private set; }

        public IReadOnlyList<DocumentationItem> DocumentationItems => _documentationItems.AsReadOnly();

        public DocumentationTemplateId TemplateId { get; private set; }

        public Documentation(DocumentationId id, string name, DocumentationTemplateId templateId, List<DocumentationItem> documentationItems, bool readOnly, bool hidden) : base(id)
        {
            Name = name;
            Category = DocumentationCategory.New();
            TemplateId = templateId;
            _documentationItems = documentationItems;
            ReadOnly = readOnly;
            Hidden = hidden;
        }

        public static Documentation Create(string name, DocumentationTemplateId templateId, List<DocumentationItem> documentationItems, bool readOnly, bool hidden)
        {
            return new(DocumentationId.CreateUnique(), name, templateId , documentationItems, readOnly, hidden);
        }

        private Documentation() {}

    }
}
