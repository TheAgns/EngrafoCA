using Application.Common;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Documentation.Commands.UpdateDocumentation
{
	public record UpdateDocumentationCommand : IRequest<Unit>
	{
		public Guid Id { get; init; }
		public string Name { get; init; }
		public string OwnerId { get; init; }
		public int DocumentationCategoryId { get; init; }
		public bool Hide { get; init; }
		public bool ReadOnly { get; init; }
	}

	// Handler
	public class UpdateDocumentationCommandHandler : IRequestHandler<UpdateDocumentationCommand, Unit>
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public UpdateDocumentationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<Unit> Handle(UpdateDocumentationCommand request, CancellationToken cancellationToken)
		{
			// Validation


			var documentation = await _dbContext.Documentations.FirstOrDefaultAsync(d => d.Id == request.Id);
			_mapper.Map(request, documentation);

			_dbContext.Documentations.Update(documentation);

			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;

		}
	}
}

