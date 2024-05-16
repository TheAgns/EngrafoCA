using Domain.Common;

namespace Domain.DocumentationAggregate.ValueObjects
{
    public sealed class DocumentationHeadingContentId : ValueObject
    {

        public Guid Value { get; }

        private DocumentationHeadingContentId(Guid value)
        {
            Value = value;
        }

		// Initializes the Id
		public static DocumentationHeadingContentId CreateUnique() => new(Guid.NewGuid());
		public static DocumentationHeadingContentId New(Guid value) => new(value);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
