using Application.Common;
using Domain.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Documentation.Queries.GetDocumentations
{
    public record GetDocumentationsQuery : IRequest<List<DocumentationDto>>
    {
    }

    // Handler
    public class GetDocumentationsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetDocumentationsQuery, List<DocumentationDto>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<DocumentationDto>> Handle(GetDocumentationsQuery request, CancellationToken cancellationToken)
        {
            var documentations = await _context.Documentations
                .AsNoTracking()
                .Select(d => _mapper.Map<DocumentationDto>(d))
                .ToListAsync();            

            //Return or throw Exception here
            if (documentations.IsNullOrEmpty())
            {
                throw new Exception();
            }

            return documentations;
        }
    }
}


