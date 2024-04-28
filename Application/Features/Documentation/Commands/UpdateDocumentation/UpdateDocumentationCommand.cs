using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Documentation.Commands.UpdateDocumentation
{
    public class UpdateDocumentationCommand : IRequest<Unit>
    {
        public UpdateDocumentationDto UpdateDocumentationDto { get; set; }

		// Handler
		public class UpdateDocumentationCommandHandler : IRequestHandler<UpdateDocumentationCommand, Unit>
		{
			private readonly IApplicationDbContext _dbContext;
			private readonly IMapper _mapper;

			public UpdateDocumentationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
			{
				_dbContext = dbContext;
				_mapper = mapper;
			}

			public async Task<Unit> Handle(UpdateDocumentationCommand request, CancellationToken cancellationToken)
			{
				// Validation


				var documentation = await _dbContext.Documentations.FirstOrDefaultAsync(d => d.Id == request.UpdateDocumentationDto.Id);
				_mapper.Map(request.UpdateDocumentationDto, documentation);

				_dbContext.Documentations.Update(documentation);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return Unit.Value;

			}
		}
	}
}
