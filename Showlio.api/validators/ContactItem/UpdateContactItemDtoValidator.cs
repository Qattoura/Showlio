using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.ContactItem
{
    public class UpdateContactItemDtoValidator
        : AbstractValidator<UpdateContactItemDto>
    {
        public UpdateContactItemDtoValidator()
        {
            RuleFor(x => x.Label)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Value)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }
    }
}
