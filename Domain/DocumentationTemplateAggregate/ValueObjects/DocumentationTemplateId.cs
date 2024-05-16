using Domain.Common;

namespace Domain.DocumentationTemplate.ValueObjects
{
    public sealed class DocumentationTemplateId : ValueObject
    {
        public Guid Value { get; }

        private DocumentationTemplateId(Guid value)
        {
            Value = value;
        }

        private DocumentationTemplateId() { }

		// Initializes the Id

		public static DocumentationTemplateId CreateUnique() => new(Guid.NewGuid());
		public static DocumentationTemplateId New(Guid id) => new(id);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
