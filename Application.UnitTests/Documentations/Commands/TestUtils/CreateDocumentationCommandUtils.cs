using Application.Features.Documentations.Commands.CreateDocumentation;
using Application.Features.Documentations.Queries;
using Application.UnitTests.TestUtils.Constants;

namespace Application.UnitTests.Documentations.Commands.TestUtils
{
	public class CreateDocumentationCommandUtils
	{
		public static CreateDocumentationCommand CreateCommand()
		{
			return new CreateDocumentationCommand
			{
				Name = Constants.Documentation.Name,
				DocumentationTemplateId = Constants.DocumentationTemplate.Id.Value,
				Category = Constants.Documentation.Category,
				DocumentationItems = CreateDocumentationItemDtos(2),
				ReadOnly = Constants.Documentation.ReadOnly,
				Hidden = Constants.Documentation.Hidden,
			};
		}

		public static List<DocumentationItemDto> CreateDocumentationItemDtos(int itemCount) =>
			Enumerable.Range(0, itemCount)
			.Select(index => new DocumentationItemDto
			{
				Content = Constants.Documentation.DocumentationItemContent,
				Position = index,
			}).ToList();
	}
}
