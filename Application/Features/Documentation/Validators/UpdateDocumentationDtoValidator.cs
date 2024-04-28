using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Documentation.Commands.UpdateDocumentation;
using FluentValidation;

namespace Application.Features.Documentation.Validators
{
    public class UpdateDocumentationDtoValidator : AbstractValidator<UpdateDocumentationDto>
    {
        public UpdateDocumentationDtoValidator()
        {
            RuleFor(p => p.Name)
                 .NotEmpty().WithMessage("{PropertyName} is required.")
                 .NotNull().WithMessage("{PropertyName} is required.")
                 .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.DocumentationCategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(p => p.Hide)
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ReadOnly)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
