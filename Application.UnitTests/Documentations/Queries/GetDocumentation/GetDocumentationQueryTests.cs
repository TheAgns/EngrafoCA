using System.Linq.Expressions;
using Application.Common.Interfaces;
using Application.Features.Documentations.Queries;
using Application.Features.Documentations.Queries.GetDocumentation;
using Application.UnitTests.Documentations.Queries.TestUtils;
using Application.UnitTests.TestUtils.Constants;
using Domain.DocumentationAggregate;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace Application.UnitTests.Documentations.Queries.GetDocumentation
{
	public class GetDocumentationQueryTests
	{
		private readonly GetDocumentationQueryHandler _handler;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<IApplicationDbContext> _dbContextMock;
		private readonly Mock<DbSet<Documentation>> _documentationDbSetMock;
		public GetDocumentationQueryTests()
		{
			_dbContextMock = new Mock<IApplicationDbContext>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetDocumentationQueryHandler(_dbContextMock.Object, _mockMapper.Object);
			_documentationDbSetMock = new Mock<DbSet<Documentation>>();
			_dbContextMock.Setup(db => db.Documentations).Returns(_documentationDbSetMock.Object);
		}
		[Fact]
		public async Task HandleGetDocumentationQuery_WhenIdIsValid_ShouldRetriveDocumentation()
		{
			//? Arrange
			var documentationItems = GetDocumentationQueryTestUtils.CreateDocumentationItems(3);
			var documentation = Documentation.Create(
				Constants.Documentation.Name,
				Constants.DocumentationTemplate.Id,
				documentationItems,
				Constants.Documentation.ReadOnly,
				Constants.Documentation.Hidden);
			var documentationDto = new DocumentationDto
			{
				Id = documentation.Id.Value,
				Name = documentation.Name,
				DocumentationItems = new List<DocumentationItemDto>
				{
					new DocumentationItemDto { Id = documentationItems[0].Id.Value, Content = documentationItems[0].Content },
					new DocumentationItemDto { Id = documentationItems[1].Id.Value, Content = documentationItems[1].Content },
					new DocumentationItemDto { Id = documentationItems[2].Id.Value, Content = documentationItems[2].Content },
				},
				DocumentationTemplateId = documentation.TemplateId.Value,
				ReadOnly = documentation.ReadOnly,
				Hidden = documentation.Hidden
			};
			/*! Won't work since we cant moq the method SingleOrDefaultAsync directly
				Alternatively use an in-memory database but this breaks clean architecture principle
			*/
			_documentationDbSetMock.Setup(db => db.FirstOrDefaultAsync(It.IsAny<Expression<Func<Documentation, bool>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(documentation);
			_mockMapper.Setup(m => m.Map<DocumentationDto>(documentation)).Returns(documentationDto);
			var query = GetDocumentationQueryTestUtils.CreateQuery(documentation.Id.Value);
			//? Act
			var result = await _handler.Handle(query, CancellationToken.None);
			//? Assert
			Assert.False(result.IsError);
			Assert.Equal(documentationDto, result.Value);
		}
	}
}