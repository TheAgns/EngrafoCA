
using Application.Common;
using Application.Features.Documentation.Commands.CreateDocumentation;
using Application.Features.Documentation.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.RegisterNewUser
{
    public class RegisterNewUserCommand : IRequest<ErrorOr<AuthenticationResult>>
	{
		public UserDto UserDto {  get; set; }

		public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, ErrorOr<AuthenticationResult>>
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

			public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
			{
				// Validation

				// Checks if there exists a User with the given email
				if (await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == command.UserDto.Email) is null)
				{
					return Errors.User.DuplicateEmail;
				}

				var user = _mapper.Map<User>(command.UserDto);

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
