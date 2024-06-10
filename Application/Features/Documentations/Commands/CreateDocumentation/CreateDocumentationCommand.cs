using Application.Common.Interfaces;
using Application.Features.Documentations.Queries;
using Domain.Common.Errors;
using Domain.DocumentationAggregate;
using Domain.DocumentationAggregate.Entities;
using Domain.DocumentationTemplate.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Documentations.Commands.CreateDocumentation
{
	//! Implement mapping from command to entity
	//? Maybe create a DTO instead of direct properties in Command
    public record CreateDocumentationCommand : IRequest<ErrorOr<Guid>>
	{
		public string Name { get; init; }
		public Guid DocumentationTemplateId { get; init; }
		public string Category {  get; init; }
		public List<DocumentationItemDto> DocumentationItems { get; init; }
		public bool Hidden { get; init; }
		public bool ReadOnly { get; init; }
	}

	// Handler
	public class CreateDocumentationCommandHandler : IRequestHandler<CreateDocumentationCommand, ErrorOr<Guid>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ILogger<CreateDocumentationCommandHandler> _logger;

		public CreateDocumentationCommandHandler(IApplicationDbContext context, ILogger<CreateDocumentationCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<ErrorOr<Guid>> Handle(CreateDocumentationCommand request, CancellationToken cancellationToken)
		{
			// Domain event is added upon creation
            var doc = Documentation.Create(
				name: request.Name,
				templateId: DocumentationTemplateId.New(request.DocumentationTemplateId),				
				documentationItems: request.DocumentationItems.ConvertAll(
					dc => DocumentationItem.Create(
						dc.Content,
						dc.Position)
					),
				readOnly: request.ReadOnly,
				hidden: request.Hidden
				);

			await _context.Documentations.AddAsync(doc);

			// Domain Event is published on SaveChangesAsync
			var numOfEntries = await _context.SaveChangesAsync(cancellationToken);

			if (numOfEntries < 1)
			{
				return Errors.Documentation.DocumentationNotCreated;
			}

			return doc.Id.Value;
		}
	}
}


