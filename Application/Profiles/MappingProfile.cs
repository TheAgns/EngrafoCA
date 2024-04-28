using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Authentication;
using Application.Features.Documentation.Queries.GetDocumentation;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            // Documentations
            CreateMap<Documentation, DocumentationDto>().ReverseMap();

            //Users
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
