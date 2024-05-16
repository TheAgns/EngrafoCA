using Application.Common.Interfaces;
using Domain.DocumentationAggregate.ValueObjects;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Documentations.Queries.GetDocumentation
{
    public record GetDocumentationQuery : IRequest<DocumentationDto>
	{
		public Guid Id { get; set; }

	}

	// Handler
	public class GetDocumentationQueryHandler : IRequestHandler<GetDocumentationQuery, DocumentationDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		public GetDocumentationQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<DocumentationDto> Handle(GetDocumentationQuery request, CancellationToken cancellationToken)
		{
			var documentation = await _context.Documentations
				.AsNoTracking()
				.SingleOrDefaultAsync(d => d.Id.Value == request.Id);

			//Return or throw exception here

			return _mapper.Map<DocumentationDto>(documentation);
		}
	}
}

