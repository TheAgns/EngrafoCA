using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using AutoMapper;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Queries.Login
{
	public class LoginQuery : IRequest<ErrorOr<AuthenticationResult>>
	{
		public LoginDto LoginDto { get; set; }

		public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
		{
			private readonly IJwtTokenGenerator _jwtTokenGenerator;
			private readonly IApplicationDbContext _dbContext;
			private readonly IMapper _mapper;

			public LoginQueryHandler(IApplicationDbContext dbContext, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
			{
				_dbContext = dbContext;
				_mapper = mapper;
				_jwtTokenGenerator = jwtTokenGenerator;
			}

			public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
			{

				// Checks if there exists a User with the given email
				if (await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == query.LoginDto.Email) is not User user)
				{
					throw new Exception("User with given email already exists");
				}

				if (user.Password != query.LoginDto.Password)
				{
					throw new Exception("Invalid Password");
				}

				var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

				return new AuthenticationResult
				(
					user.Id,
					user.FirstName,
					user.LastName,
					query.LoginDto.Email,
					token
				);

			}
		}
    }
}
