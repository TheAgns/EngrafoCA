using Domain.Common.Abstractions;
using Domain.DocumentationTemplate.ValueObjects;

namespace Domain.DocumentationAggregate.ValueObjects
{
    public sealed class DocumentationId : ValueObject
    {
        public Guid Value { get; }

        private DocumentationId(Guid value)
        {
            Value = value;
        }

		// Initializes the Id

		public static DocumentationId CreateUnique() => new(Guid.NewGuid());
		public static DocumentationId New(Guid value) => new(value);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
