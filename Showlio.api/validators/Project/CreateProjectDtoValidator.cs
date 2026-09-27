using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.Project
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Image)
                .MaximumLength(500);

            RuleFor(x => x.ProjectLink)
                .MaximumLength(500);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }
    }
}
