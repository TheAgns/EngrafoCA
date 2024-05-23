using Domain.Common.Abstractions;

namespace Domain.DocumentationAggregate.ValueObjects
{
    public sealed class DocumentationItemId : ValueObject
    {

        public Guid Value { get; }

        private DocumentationItemId(Guid value)
        {
            Value = value;
        }

		// Initializes the Id
		public static DocumentationItemId CreateUnique() => new(Guid.NewGuid());
		public static DocumentationItemId New(Guid value) => new(value);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
