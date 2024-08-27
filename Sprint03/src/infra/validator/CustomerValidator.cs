namespace Sprint03.infra.validator;

using FluentValidation;
using domain.model;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("Name cannot be null or empty.");

        RuleFor(customer => customer.Document)
            .NotEmpty().WithMessage("Document cannot be null or empty.");

        RuleFor(customer => customer.Cep)
            .NotEmpty().WithMessage("CEP cannot be null or empty.");

        RuleFor(customer => customer.BirthDate)
            .NotEqual(DateTime.MinValue).WithMessage("BirthDate cannot be null or empty.");

        RuleFor(customer => customer.AgreementId)
            .GreaterThan(0).WithMessage("AgreementId must be greater than zero.");
    }
}