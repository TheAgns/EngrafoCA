using FluentValidation;

namespace Application.Features.Documentations.Commands.CreateDocumentation
{
    public class CreateDocumentationCommandValidator : AbstractValidator<CreateDocumentationCommand>
    {
        public CreateDocumentationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.DocumentationTemplateId).NotEmpty();
            RuleFor(x => x.DocumentationTemplateId).NotNull();
        }
    }
}
