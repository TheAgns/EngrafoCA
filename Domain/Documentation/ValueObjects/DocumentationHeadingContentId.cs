using Domain.Common;

namespace Domain.Documentation.ValueObjects
{
    public sealed class DocumentationHeadingContentId : ValueObject
    {

        public Guid Value { get; }

        private DocumentationHeadingContentId(Guid value)
        {
            Value = value;
        }

        // Initializes the Id
        public static DocumentationHeadingContentId New() => new(Guid.NewGuid());
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
