using Domain.Common;
using Domain.Documentation.Enums;
using Domain.Documentation.ValueObjects;

namespace Domain.Documentation.Entities
{
    public sealed class DocumentationHeadingContent : BaseEntity<DocumentationHeadingContentId>
    {
        public ContentType ContentType { get; } 

        public object? Content { get; }

        public int Position { get; }

        private DocumentationHeadingContent(DocumentationHeadingContentId id, ContentType contentType, Object content, int position) : base(id)
        {
            ContentType = contentType;
            Content = content;
            Position = position;
        }

        public static DocumentationHeadingContent Create(ContentType contentType, Object content,int position)
        {
            return new (DocumentationHeadingContentId.New(), contentType, content, position);
        }


    }
}
