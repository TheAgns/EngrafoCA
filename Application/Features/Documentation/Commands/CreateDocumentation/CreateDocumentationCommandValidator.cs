using FluentValidation;

namespace Application.Features.Documentation.Commands.CreateDocumentation
{
    public class CreateDocumentationCommandValidator : AbstractValidator<CreateDocumentationCommand>
    {
        public CreateDocumentationCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
