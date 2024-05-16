using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Documentations.Commands.DeleteDocumentation
{
    public record DeleteDocumentationCommand : IRequest<Unit>
	{
		public Guid Id { get; init; }
	}

	// Handler
	public class DeleteDocumentationCommandHandler : IRequestHandler<DeleteDocumentationCommand, Unit>
	{
		private readonly IApplicationDbContext _dbContext;

		public DeleteDocumentationCommandHandler(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(DeleteDocumentationCommand request, CancellationToken cancellationToken)
		{
			var documentation = await _dbContext.Documentations.FirstOrDefaultAsync(d => d.Id.Value == request.Id);

			_dbContext.Documentations.Remove(documentation);

			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}

	}
}

