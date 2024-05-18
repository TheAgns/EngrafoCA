using System.Diagnostics;
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
		public Guid Id { get; init; }

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
			Debug.WriteLine(request.Id);

            var docId = DocumentationId.New(request.Id);

            var doc = await _context.Documentations
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == docId, cancellationToken);

            //Return or throw exception here
            if (doc is null)
			{
				throw new Exception("No Documentation with that id was found");
			}

			return _mapper.Map<DocumentationDto>(doc);
		}
	}
}

