using FluentValidation;
using Sprint03.domain.model;

namespace Sprint03.infra.validator
{
    public class AgreementValidator : AbstractValidator<Agreement>
    {
        public AgreementValidator()
        {
            RuleFor(agreement => agreement.Name)
                .NotEmpty().WithMessage("Name cannot be null or empty.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(agreement => agreement.Value)
                .GreaterThan(0).WithMessage("Value must be greater than zero.");

            RuleFor(agreement => agreement.ServiceType)
                .NotEmpty().WithMessage("ServiceType cannot be null or empty.")
                .MaximumLength(100).WithMessage("ServiceType cannot exceed 100 characters.");

            RuleFor(agreement => agreement.Coverage)
                .NotEmpty().WithMessage("Coverage cannot be null or empty.")
                .MaximumLength(100).WithMessage("Coverage cannot exceed 100 characters.");
        }
    }
}