using Domain.Common;
using Domain.DocumentationAggregate.ValueObjects;

namespace Domain.DocumentationAggregate.Entities
{
    public sealed class DocumentationItem : BaseEntity<DocumentationItemId>
    {
        // TODO: Make Enum and conversion
        public string ContentType { get; } 

        public string Content { get; }

        public int Position { get; }

        private DocumentationItem() {}

        private DocumentationItem(DocumentationItemId id, string content, int position) : base(id)
        {
            Content = content;
            Position = position;
		}

        public static DocumentationItem Create(string content, int position)
        {
            return new (DocumentationItemId.CreateUnique(), content, position);
        }


    }
}
