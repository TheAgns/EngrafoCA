using Domain.Common;

namespace Domain.DocumentationTemplate.ValueObjects
{
    public sealed class DocumentationTemplateHeading : ValueObject
    {
        public string Title { get; private set; }
        public int Position { get; private set; }

        private DocumentationTemplateHeading() {}

        private DocumentationTemplateHeading(string title, int position)
        {
            Title = title;
            Position = position;
        }

        public static DocumentationTemplateHeading Create(string title, int position)
        {
            return new(title, position);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
            yield return Position;
        }
    }
}
