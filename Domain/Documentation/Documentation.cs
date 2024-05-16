using Domain.Common;
using Domain.Documentation.Entities;
using Domain.Documentation.ValueObjects;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.Documentation
{

    public sealed class Documentation : AggregateRoot<DocumentationId>
    {
        // The list containing the content of the documentation
        private readonly List<DocumentationHeadingContent> _documentationHeadingContents = new();
        public string Name { get; private set; }

        public DocumentationCategory Category { get; private set; }

        public bool ReadOnly { get; private set; }

        public bool Hidden { get; private set; }

        public IReadOnlyList<DocumentationHeadingContent> DocumentationHeadingContents => _documentationHeadingContents.ToList();

        public DocumentationTemplateId TemplateId { get; private set; }
    
        private Documentation(DocumentationId id, string name, DocumentationCategory category, DocumentationTemplateId templateId, List<DocumentationHeadingContent> documentationHeadingContents, bool readOnly, bool hidden) : base(id)
        {
            Name = name;
            Category = category;
            TemplateId = templateId;
            _documentationHeadingContents = documentationHeadingContents;
            ReadOnly = readOnly;
            Hidden = hidden;
        }

        public static Documentation Create(string name, DocumentationCategory category, DocumentationTemplateId templateId, List<DocumentationHeadingContent> documentationHeadingContents, bool readOnly, bool hidden)
        {
            return new(DocumentationId.New(), name, category, templateId , documentationHeadingContents , readOnly, hidden);
        }
    }
}
