using Domain.DocumentationAggregate.ValueObjects;

namespace Application.UnitTests.TestUtils.Constants
{
	public static partial class Constants
	{

		public static class Documentation
		{
			public const string Name = "Documentation Name";

			public static readonly DocumentationCategory Category = DocumentationCategory.New();

			public const bool ReadOnly = false;

			public const bool Hidden = false;
			
			public const string DocumentationItemContent = "Content";

			public static string DocumentationItemContentFromIndex(int index) => $"{DocumentationItemContent} {index}";
		}
	}
}
