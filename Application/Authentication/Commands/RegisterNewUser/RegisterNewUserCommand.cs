using Application.Common;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.RegisterNewUser
{
	public record RegisterNewUserCommand : IRequest<ErrorOr<AuthenticationResponse>>
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }

	}

	public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, ErrorOr<AuthenticationResponse>>
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

		public async Task<ErrorOr<AuthenticationResponse>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
		{
			// Validation

			// Checks if there exists a User with the given email
			if (await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == command.Email) is null)
			{
				return Errors.User.DuplicateEmail;
			}

			var user = _mapper.Map<User>(command);

			_dbContext.Users.Add(user);

			await _dbContext.SaveChangesAsync(cancellationToken);

			var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

			//TODO: Refactor to not return any data (since it is a commmand)
			return new AuthenticationResponse(
				user.Id,
				user.FirstName,
				user.LastName,
				user.Email,
				token
			);
		}
	}

}
