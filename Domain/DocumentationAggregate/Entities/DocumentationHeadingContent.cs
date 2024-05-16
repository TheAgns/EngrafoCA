using Domain.Common;
using Domain.DocumentationAggregate.ValueObjects;

namespace Domain.DocumentationAggregate.Entities
{
    public sealed class DocumentationHeadingContent : BaseEntity<DocumentationHeadingContentId>
    {
        // TODO: Make Enum and conversion
        public string ContentType { get; } 

        public string Content { get; }

        public int Position { get; }

        private DocumentationHeadingContent() {}

        private DocumentationHeadingContent(DocumentationHeadingContentId id, string content, int position, string contentType) : base(id)
        {
            Content = content;
            Position = position;
			ContentType = contentType;
		}

        public static DocumentationHeadingContent Create(string content, int position, string contentType)
        {
            return new (DocumentationHeadingContentId.CreateUnique(), content, position, contentType);
        }


    }
}
