using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using MapsterMapper;
using MediatR;

namespace Application.Features.Documentation.Commands.CreateDocumentation
{
	public record CreateDocumentationCommand : IRequest<Guid>
	{
		public string Name { get; init; }
		public string OwnerId { get; init; }
		public int DocumentationTemplateId { get; init; }
		public int DocumentationCategoryId { get; init; }
		public bool Hide { get; init; }
		public bool ReadOnly { get; init; }
	}

	// Handler
	public class CreateDocumentationCommandHandler : IRequestHandler<CreateDocumentationCommand, Guid>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateDocumentationCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Guid> Handle(CreateDocumentationCommand request, CancellationToken cancellationToken)
		{
			
			var documentation = _mapper.Map<Domain.Entities.Documentation>(request);

			_context.Documentations.Add(documentation);

			await _context.SaveChangesAsync(cancellationToken);

			return documentation.Id;
		}
	}
}


