using Application.Common.Interfaces;
using Domain.DocumentationTemplate.ValueObjects;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Common.Errors;

namespace Application.Features.DocumentationTemplates.Queries.GetDocumentationTemplate
{
	public record GetDocumentationTemplateQuery : IRequest<ErrorOr<DocumentationTemplateDto>>
	{
		public Guid Id { get; init; }
	}

	// Handler
	public class GetDocumentationTemplateQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetDocumentationTemplateQuery, ErrorOr<DocumentationTemplateDto>>
	{
		private readonly IApplicationDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task<ErrorOr<DocumentationTemplateDto>> Handle(GetDocumentationTemplateQuery request, CancellationToken cancellationToken)
		{

			var templateId = DocumentationTemplateId.New(request.Id);

			var template = await _context.DocumentationTemplates
				.AsNoTracking()
				.SingleOrDefaultAsync(t => t.Id == templateId, cancellationToken);
				

			//Return or throw exception here
			if (template is null)
			{
				return Errors.DocumentationTemplate.TemplateNotFound;
			}

			return _mapper.Map<DocumentationTemplateDto>(template);

		}
	}
}
