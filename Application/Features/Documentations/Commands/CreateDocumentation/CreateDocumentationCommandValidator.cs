using FluentValidation;

namespace Application.Features.Documentations.Commands.CreateDocumentation
{
    public class CreateDocumentationCommandValidator : AbstractValidator<CreateDocumentationCommand>
    {
        public CreateDocumentationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.DocumentationTemplateId).NotEmpty().WithMessage("A Template must be selected");
            RuleFor(x => x.DocumentationTemplateId).NotNull();
        }
    }
}
