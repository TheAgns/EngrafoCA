using Application.Common.Interfaces;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Domain.Common.Errors;

namespace Application.Features.DocumentationTemplates.Queries.GetDocumentationTemplates
{
    public record GetDocumentationTemplatesQuery : IRequest<ErrorOr<List<DocumentationTemplateDto>>>
    {
    }

    // Handler
    public class GetDocumentationTemplatesQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetDocumentationTemplatesQuery, ErrorOr<List<DocumentationTemplateDto>>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<ErrorOr<List<DocumentationTemplateDto>>> Handle(GetDocumentationTemplatesQuery request, CancellationToken cancellationToken)
        {
            var templates = await _context.DocumentationTemplates
                .AsNoTracking()
                .Include(t => t.DocumentationTemplateHeadings)
                .Select(t => new DocumentationTemplateDto
                {
                    Id = t.Id.Value,
                    Title = t.Title,
                    DocumentationTemplateHeadings = t.DocumentationTemplateHeadings
                        .Select(h => _mapper.Map<DocumentationTemplateHeadingDto>(h))
                        .ToList()

                })
            .ToListAsync(cancellationToken);

            if (templates.IsNullOrEmpty()) 
            {
                return Errors.DocumentationTemplate.TemplateNotFound;
            }

            return templates;
        
        }
    }
}
