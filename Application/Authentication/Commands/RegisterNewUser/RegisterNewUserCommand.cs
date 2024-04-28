using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Features.Documentation.Commands.CreateDocumentation;
using Application.Features.Documentation.Validators;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.RegisterNewUser
{
    public class RegisterNewUserCommand : IRequest<AuthenticationResult>
	{
		public UserDto UserDto {  get; set; }

		public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, AuthenticationResult>
		{
			private readonly IApplicationDbContext _dbContext;
			private readonly IMapper _mapper;
			private readonly IJwtTokenGenerator _jwtTokenGenerator;

			public RegisterNewUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
			{
				_dbContext = dbContext;
				_mapper = mapper;
				_jwtTokenGenerator = jwtTokenGenerator;
			}

			public async Task<AuthenticationResult> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
			{
				// Validation

				// Checks if there exists a User with the given email
				if (await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == request.UserDto.Email) is null)
				{
					throw new Exception("User with given email already exists");
				}

				var user = _mapper.Map<User>(request.UserDto);

				_dbContext.Users.Add(user);

				await _dbContext.SaveChangesAsync(cancellationToken);

				var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);


				return new AuthenticationResult
				(
					user.Id,
					user.FirstName,
					user.LastName,
					user.Email,
					token
				);
			}
		}
	}
}
