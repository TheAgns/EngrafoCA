using Domain.Common.Abstractions;
using Domain.DocumentationAggregate.ValueObjects;

namespace Domain.DocumentationAggregate.Entities
{
    public sealed class DocumentationItem : BaseEntity<DocumentationItemId>
    {
        // TODO: Make Enum and conversion
        //public string ContentType { get; private set; } 

        public string Content { get; private set; }

        public int Position { get; private set; }

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
