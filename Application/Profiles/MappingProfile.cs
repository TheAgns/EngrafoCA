using Application.Authentication;
using Application.Features.Documentation.Queries.GetDocumentation;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{

    //TODO: Refactor mapping into DTO classes
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
