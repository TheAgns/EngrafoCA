using Domain.Common;

namespace Domain.Documentation.ValueObjects
{
    public sealed class DocumentationId : ValueObject
    {
        public Guid Value { get; }

        private DocumentationId(Guid value)
        {
            Value = value;
        }

        // Initializes the Id
        public static DocumentationId New() => new(Guid.NewGuid());
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
