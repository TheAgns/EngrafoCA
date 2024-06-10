using Application.Features.Documentations.Queries.GetDocumentation;
using Application.UnitTests.TestUtils.Constants;
using Domain.DocumentationAggregate.Entities;
namespace Application.UnitTests.Documentations.Queries.TestUtils
{
	public class GetDocumentationQueryTestUtils
	{
		public static GetDocumentationQuery CreateQuery(Guid id)
		{
			return new GetDocumentationQuery
			{
				Id = id
			};
		}
		public static List<DocumentationItem> CreateDocumentationItems(int itemCount = 2) =>
			Enumerable.Range(0, itemCount)
			.Select(index => DocumentationItem.Create(
				Constants.Documentation.DocumentationItemContentFromIndex(index),
				index
			)).ToList();
	}
}