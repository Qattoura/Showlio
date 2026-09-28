using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.Validators.Education
{
    public class UpdateEducationDtoValidator
        : AbstractValidator<UpdateEducationDto>
    {
        public UpdateEducationDtoValidator()
        {
            RuleFor(x => x.Institution)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Degree)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Field)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }
    }
}