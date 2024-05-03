using Application.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.Login
{
	public record LoginQuery : IRequest<ErrorOr<AuthenticationResponse>>
	{
		public string Email { get; init; }
		public string Password { get; init; }

	}

	public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResponse>>
	{
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly IApplicationDbContext _dbContext;

		public LoginQueryHandler(IApplicationDbContext dbContext, IJwtTokenGenerator jwtTokenGenerator)
		{
			_dbContext = dbContext;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
		{

			// Checks if there exists a User with the given email
			if (await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == query.Email) is not User user)
			{
				throw new Exception("User with given email already exists");
			}

			if (user.Password != query.Password)
			{
				throw new Exception("Invalid Password");
			}

			var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

			return new AuthenticationResponse
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
