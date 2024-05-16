using Domain.Common;

namespace Domain.Documentation.ValueObjects
{
    public sealed class DocumentationCategory : ValueObject
    {
        public string Value { get; private set; }

        private DocumentationCategory(string value)
        {
            Value = value;
        }

        public static DocumentationCategory New(string title) => new(title);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
