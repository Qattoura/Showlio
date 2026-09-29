using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.Template
{
    public class UpdateTemplateValidator : AbstractValidator<UpdateTemplateDto>
    {
        public UpdateTemplateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Genre)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.ColorTheme)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
