using FluentValidation;
using Showlio.api.Dtos;

namespace Showlio.api.validators.Portfolio
{
    public class CreatePortfolioDtoValidator : AbstractValidator<CreatePortfolioDto>
    {
        public CreatePortfolioDtoValidator()
        {
            RuleFor(x => x.PortfolioName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.TemplateId)
                .GreaterThan(0);
        }
    }
}