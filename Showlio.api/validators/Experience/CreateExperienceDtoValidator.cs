using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.Experience
{
    public class CreateExperienceDtoValidator : AbstractValidator<CreateExperienceDto>
    {
        public CreateExperienceDtoValidator()
        {
            RuleFor(x => x.Company)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Position)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue);

            RuleFor(x => x)
                .Must(x => x.IsCurrent || x.EndDate.HasValue)
                .WithMessage("EndDate is required when the experience is not current.");

            RuleFor(x => x)
                .Must(x => !x.IsCurrent || !x.EndDate.HasValue)
                .WithMessage("EndDate must be null when the experience is current.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }
    }
}