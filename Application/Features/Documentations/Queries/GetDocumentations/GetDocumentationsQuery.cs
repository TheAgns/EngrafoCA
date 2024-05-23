using Application.Common.Interfaces;
using Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Documentations.Queries.GetDocumentations
{
    public record GetDocumentationsQuery : IRequest<ErrorOr<List<DocumentationDto>>>
    {
    }

    // Handler
    public class GetDocumentationsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetDocumentationsQuery, ErrorOr<List<DocumentationDto>>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ErrorOr<List<DocumentationDto>>> Handle(GetDocumentationsQuery request, CancellationToken cancellationToken)
        {
            var documentations = await _context.Documentations
                .AsNoTracking()
                .Select(d => _mapper.Map<DocumentationDto>(d))
                .ToListAsync();            

            // Return or throw Exception here
            if (documentations.IsNullOrEmpty())
            {
                return Errors.Documentation.DocumentationNotFound;
            }

            return documentations;
        }
    }
}


