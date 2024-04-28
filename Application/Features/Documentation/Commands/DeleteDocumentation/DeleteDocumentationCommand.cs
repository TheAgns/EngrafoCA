using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Documentation.Commands.DeleteDocumentation
{
    public class DeleteDocumentationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

		// Handler
		public class DeleteDocumentationCommandHandler : IRequestHandler<DeleteDocumentationCommand, Unit>
		{
			private readonly IApplicationDbContext _dbContext;
			private readonly IMapper _mapper;

			public DeleteDocumentationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
			{
				_dbContext = dbContext;
				_mapper = mapper;
			}

			public async Task<Unit> Handle(DeleteDocumentationCommand request, CancellationToken cancellationToken)
			{
				var documentation = await _dbContext.Documentations.FirstOrDefaultAsync(d => d.Id == request.Id);

				_dbContext.Documentations.Remove(documentation);

				await _dbContext.SaveChangesAsync(cancellationToken);

				return Unit.Value;
			}

		}
	}
}
