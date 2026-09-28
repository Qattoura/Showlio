using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.Certificate
{
    public class CreateCertificateDtoValidator
        : AbstractValidator<CreateCertificateDto>
    {
        public CreateCertificateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Image)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Image));

            RuleFor(x => x.CertificateLink)
                .MaximumLength(500)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.CertificateLink))
                .WithMessage("CertificateLink must be a valid URL.");

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(
                url,
                UriKind.Absolute,
                out var result)
                && (result.Scheme == Uri.UriSchemeHttp
                    || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}