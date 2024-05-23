using Application.Features.Documentations.Commands.CreateDocumentation;
using Application.Features.Documentations.Queries;
using Application.UnitTests.TestUtils.Constants;

namespace Application.UnitTests.Documentations.Commands.TestUtils
{
	//! Generates the required objects (commands, dtos etc.) using Constants for primitive values
	public class CreateDocumentationCommandUtils
	{
		//? Accepts the items needed to create the inner object (DocumentationItems)
		//? If none are passed, it defaults to: itemCount = 2
		public static CreateDocumentationCommand CreateCommand(List<DocumentationItemDto>? items = null)
		{
			return new CreateDocumentationCommand
			{
				Name = Constants.Documentation.Name,
				DocumentationTemplateId = Constants.DocumentationTemplate.Id.Value,
				Category = Constants.Documentation.Category.Value,
				DocumentationItems = items ?? CreateDocumentationItemDtos(),
				ReadOnly = Constants.Documentation.ReadOnly,
				Hidden = Constants.Documentation.Hidden,
			};
		}

		public static List<DocumentationItemDto> CreateDocumentationItemDtos(int itemCount = 2) =>
			Enumerable.Range(0, itemCount)
			.Select(index => new DocumentationItemDto
			{
				Content = Constants.Documentation.DocumentationItemContentFromIndex(index),
				Position = index,
			}).ToList();
	}
}
