using FluentValidation;
using Sprint03.domain.model;

namespace Sprint03.infra.validator
{
    public class UnitValidator : AbstractValidator<Unit>
    {
        public UnitValidator()
        {
            RuleFor(unit => unit.Name)
                .NotEmpty().WithMessage("Name cannot be null or empty.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(unit => unit.Phone)
                .NotEmpty().WithMessage("Phone cannot be null or empty.")
                .MaximumLength(15).WithMessage("Phone cannot exceed 15 characters.")
                .Matches(@"^\d+$").WithMessage("Phone must be a valid number.");

            RuleFor(unit => unit.Email)
                .NotEmpty().WithMessage("Email cannot be null or empty.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(unit => unit.Type)
                .NotEmpty().WithMessage("Type cannot be null or empty.")
                .MaximumLength(50).WithMessage("Type cannot exceed 50 characters.");

            RuleFor(unit => unit.Cep)
                .NotEmpty().WithMessage("CEP cannot be null or empty.")
                .Length(8).WithMessage("CEP must be exactly 8 characters.")
                .Matches(@"^\d{8}$").WithMessage("CEP must be a valid number with 8 digits.");
        }
    }
}