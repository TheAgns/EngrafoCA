using Application.Common.Interfaces;
using Application.Features.Documentations.Commands.CreateDocumentation;
using Application.UnitTests.Documentations.Commands.TestUtils;
using FluentAssertions;
using Moq;
using Domain.Common.Errors;
using Domain.DocumentationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.UnitTests.Documentations.Commands.CreateDocumentation
{
	public class CreateDocumentationCommandTests
	{

		private readonly CreateDocumentationCommandHandler _handler;

		private readonly Mock<IApplicationDbContext> _dbContextMock;

		private readonly Mock<DbSet<Documentation>> _documentationDbSetMock;

		public CreateDocumentationCommandTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _handler = new CreateDocumentationCommandHandler(_dbContextMock.Object);
			_documentationDbSetMock = new Mock<DbSet<Documentation>>();

			_dbContextMock.Setup(db => db.Documentations).Returns(_documentationDbSetMock.Object);
        }

		//! NAMING - Seperated by _
		//! T1: System Under Test - The feature we are testing
		//! T2: Scenario - Different cases
		//! T3: Expected Outcome

		//! STRUCTURE - Arrange, Act, Assert

		[Theory]
		[MemberData(nameof(ValidCreateDocumentationCommands))]        
        public async Task HandleCreateDocumentationCommand_WhenDocumentationIsValid_ShouldCreateDocumentationAndReturnId(CreateDocumentationCommand createDocumentationCommand)
		{
            //? Arrange - [Theory] and [MemberData] sets up the data use in the test using the ValidCreateDocumentationCommands()
			Documentation capturedDocumentation = null;

			/*? Mocks the AddAsync Method of the dbContext.Documentations
			    Then captures the object passed to the addAsync method
			    then returns a ValueTask (indicating that the operation completed)
			*/
			_documentationDbSetMock.Setup(db => db.AddAsync(It.IsAny<Documentation>(), It.IsAny<CancellationToken>()))
			.Callback<Documentation, CancellationToken>((doc, _) => capturedDocumentation = doc)
			.Returns(new ValueTask<EntityEntry<Documentation>>());

			_dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);

			//? Act
			var res = await _handler.Handle(createDocumentationCommand, default);

			//? Assert
			res.IsError.Should().BeFalse();
			res.Value.Should().NotBeEmpty();

			_dbContextMock.Verify(x => x.Documentations.AddAsync(It.Is<Documentation>(d => d.Id.Value == res.Value), It.IsAny<CancellationToken>()), Times.Once);
		}

		[Theory]
		[MemberData(nameof(ValidCreateDocumentationCommands))]
		public async Task HandleCreateDocumentationCommand_WhenDocumentationIsInvalid_ShouldReturnError(CreateDocumentationCommand createDocumentationCommand)
		{

			//? Act
			var res = await _handler.Handle(createDocumentationCommand, default);

			//? Assert
			res.IsError.Should().BeTrue();
			res.FirstError.Should().Be(Errors.Documentation.DocumentationNotCreated);
		}

		//! Creates a command with a specified number of created DocumentationItemDtos
		public static IEnumerable<object[]> ValidCreateDocumentationCommands()
		{
			yield return new[] {
				CreateDocumentationCommandUtils.CreateCommand(
				items: CreateDocumentationCommandUtils.CreateDocumentationItemDtos(itemCount: 4))
			};
		} 
	}
}
