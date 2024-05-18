using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Features.Documentations.Queries;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DocumentationTemplates.Queries.GetDocumentationTemplates
{
    public record GetDocumentationTemplatesQuery : IRequest<List<DocumentationTemplateDto>>
    {
    }

    // Handler
    public class GetDocumentationTemplatesQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetDocumentationTemplatesQuery, List<DocumentationTemplateDto>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<DocumentationTemplateDto>> Handle(GetDocumentationTemplatesQuery request, CancellationToken cancellationToken)
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

            return templates;
        
        }
    }
}
