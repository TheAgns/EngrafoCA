using Application.Common.Interfaces;
using Domain.DocumentationAggregate;
using Domain.DocumentationAggregate.Entities;
using Domain.DocumentationAggregate.ValueObjects;
using Domain.DocumentationTemplate.ValueObjects;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace Application.Features.Documentations.Commands.CreateDocumentation
{
	//! Implement mapping from command to entity
	//? Maybe create a DTO instead of direct properties in Command
    public record CreateDocumentationCommand : IRequest<Guid>
	{
		public string Name { get; init; }
		public Guid DocumentationTemplateId { get; init; }
		public string Category {  get; init; }

		public List<DocumentationHeadingContent> DocumentationHeadingContents { get; init; }
		public bool Hidden { get; init; }
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

			var doc = Documentation.Create(
				name: request.Name,
				templateId: DocumentationTemplateId.New(request.DocumentationTemplateId),				
				documentationHeadingContents: request.DocumentationHeadingContents.ConvertAll(
					dc => DocumentationHeadingContent.Create(
						dc.Content,
						dc.Position,
						dc.ContentType)
					),
				readOnly: request.ReadOnly,
				hidden: request.Hidden
				);

			_context.Documentations.Add(doc);

			await _context.SaveChangesAsync(cancellationToken);

			return doc.Id.Value;
		}
	}
}


