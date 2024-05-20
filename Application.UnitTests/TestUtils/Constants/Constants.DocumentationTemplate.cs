using Domain.DocumentationTemplate.ValueObjects;

namespace Application.UnitTests.TestUtils.Constants
{
	public static partial class Constants
	{

		public static class DocumentationTemplate
		{
			public static readonly DocumentationTemplateId Id = DocumentationTemplateId.CreateUnique();

			public const string Title = "Template Title";

			public const string Category = "Documentation Category";

		}
	}
}
