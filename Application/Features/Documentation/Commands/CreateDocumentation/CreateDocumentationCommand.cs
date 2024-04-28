using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.Documentation.Validators;
using AutoMapper;
using MediatR;

namespace Application.Features.Documentation.Commands.CreateDocumentation
{
    public class CreateDocumentationCommand : IRequest<Guid>
    {
        public CreateDocumentationDto CreateDocumentationDto { get; set; }

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
				var validator = new CreateDocumentationDtoValidator();
				var validationresult = await validator.ValidateAsync(request.CreateDocumentationDto);

				if (validationresult.IsValid == false)
				{
					throw new Exception();
				}

				var documentation = _mapper.Map<Domain.Entities.Documentation>(request.CreateDocumentationDto);

				_context.Documentations.Add(documentation);

				await _context.SaveChangesAsync(cancellationToken);

				return documentation.Id;
			}
		}
	}
       
}
