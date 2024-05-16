using Domain.Common;

namespace Domain.DocumentationAggregate.ValueObjects
{
    public sealed class DocumentationCategory : ValueObject
    {
        public string Value { get; private set; }

        private DocumentationCategory(string value)
        {
            Value = value;
        }

        private DocumentationCategory() {}
        public static DocumentationCategory New(string value = "Default") => new(value);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
