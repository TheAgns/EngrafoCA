using Application.Common;
using Domain.Documentation.ValueObjects;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace Application.Features.Documentation.Commands.CreateDocumentation
{
    public record CreateDocumentationCommand : IRequest<DocumentationId>
	{
		public string Name { get; init; }
		public Guid DocumentationTemplateId { get; init; }
		public bool Hide { get; init; }
		public bool ReadOnly { get; init; }
	}

	// Handler
	public class CreateDocumentationCommandHandler : IRequestHandler<CreateDocumentationCommand, DocumentationId>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateDocumentationCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<DocumentationId> Handle(CreateDocumentationCommand request, CancellationToken cancellationToken)
		{

			var documentation = _mapper.Map<Domain.Documentation.Documentation>(request);

			_context.Documentations.Add(documentation);

			await _context.SaveChangesAsync(cancellationToken);

			return documentation.Id;
		}
	}
}


