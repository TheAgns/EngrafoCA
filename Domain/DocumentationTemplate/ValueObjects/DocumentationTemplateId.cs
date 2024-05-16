using Domain.Common;
using Domain.Documentation.ValueObjects;

namespace Domain.DocumentationTemplate.ValueObjects
{
    public sealed class DocumentationTemplateId : ValueObject
    {
        public Guid Value { get; }

        private DocumentationTemplateId(Guid value)
        {
            Value = value;
        }

        // Initializes the Id
        public static DocumentationTemplateId New() => new(Guid.NewGuid());
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
