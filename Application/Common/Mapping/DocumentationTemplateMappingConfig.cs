using Application.Features.DocumentationTemplates.Queries;
using Domain.DocumentationTemplate;
using Domain.DocumentationTemplate.ValueObjects;
using Mapster;

namespace Application.Common.Mapping
{
    public class DocumentationTemplateMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DocumentationTemplate, DocumentationTemplateDto>()
                .Map(dest => dest.Id, src => src.Id.Value);
        }
    }
}
