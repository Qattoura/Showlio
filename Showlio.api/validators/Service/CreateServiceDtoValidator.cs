using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.Service
{
    public class CreateServiceDtoValidator
        : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Link)
                .Must(link =>
                    string.IsNullOrWhiteSpace(link) ||
                    Uri.TryCreate(
                        link,
                        UriKind.Absolute,
                        out var uri) &&
                    (uri.Scheme == Uri.UriSchemeHttp ||
                     uri.Scheme == Uri.UriSchemeHttps))
                .WithMessage("Link must be a valid URL.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }
    }
}
