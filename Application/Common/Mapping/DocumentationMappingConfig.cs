using Application.Features.Documentations.Queries;
using Domain.DocumentationAggregate;
using Domain.DocumentationAggregate.Entities;
using Mapster;

namespace Application.Common.Mapping
{
    public class DocumentationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Documentation, DocumentationDto>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.DocumentationTemplateId, src => src.TemplateId.Value);

            config.NewConfig<DocumentationItem, DocumentationItemDto>()
                .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
