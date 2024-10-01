using Xunit;
using FluentValidation.TestHelper;
using Sprint03.domain.model;
using Sprint03.infra.validator;

public class CustomerValidatorTest
{
    private readonly CustomerValidator _validator;

    public CustomerValidatorTest()
    {
        _validator = new CustomerValidator();
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsEmpty()
    {
        var customer = new Customer { Name = string.Empty };

        var result = _validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenNameIsProvided()
    {
        var customer = new Customer { Name = "John Doe" };

        var result = _validator.TestValidate(customer);

        result.ShouldNotHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenDocumentIsEmpty()
    {
        var customer = new Customer { Document = string.Empty };

        var result = _validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(c => c.Document);
    }

    [Fact]
    public void ShouldHaveErrorWhenCepIsEmpty()
    {
        var customer = new Customer { Cep = string.Empty };

        var result = _validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(c => c.Cep);
    }

    [Fact]
    public void ShouldHaveErrorWhenBirthDateIsMinValue()
    {
        var customer = new Customer { BirthDate = DateTime.MinValue };

        var result = _validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(c => c.BirthDate);
    }

    [Fact]
    public void ShouldHaveErrorWhenAgreementIdIsZeroOrNegative()
    {
        var customer = new Customer { AgreementId = 0 };

        var result = _validator.TestValidate(customer);

        result.ShouldHaveValidationErrorFor(c => c.AgreementId);
    }
}
