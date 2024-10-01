using System.Reflection;
using Xunit;
using Moq;
using FluentValidation;
using Sprint03.domain.model;
using Sprint03.adapter.input;
using Sprint03.infra.exception;
using FluentValidation.Results;
using Sprint03.domain.useCase.dto;

public class CustomerAdapterTest
{
    private readonly Mock<ICustomerUseCase> _customerUseCaseMock;
    private readonly Mock<IValidator<Customer>> _customerValidatorMock;
    private readonly CustomerAdapter _customerAdapter;

    public CustomerAdapterTest()
    {
        _customerUseCaseMock = new Mock<ICustomerUseCase>();
        _customerValidatorMock = new Mock<IValidator<Customer>>();
        _customerAdapter = new CustomerAdapter(_customerUseCaseMock.Object, _customerValidatorMock.Object);
    }

    [Fact]
    public void FindById_ShouldReturnCustomer_WhenIdIsValid()
    {
        var customerId = Guid.NewGuid().ToString();
        var expectedCustomer = new Customer { Id = customerId };
        _customerUseCaseMock.Setup(x => x.FindById(customerId)).Returns(expectedCustomer);

        var result = _customerAdapter.FindById(customerId);

        Assert.Equal(expectedCustomer, result);
    }

    [Fact]
    public void FindById_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        var customerId = Guid.NewGuid().ToString();
        _customerUseCaseMock.Setup(x => x.FindById(customerId)).Returns((Customer)null);

        Assert.Throws<NotFoundException>(() => _customerAdapter.FindById(customerId));
    }

    [Fact]
    public void Create_ShouldThrowInvalidException_WhenCustomerIsInvalid()
    {
        var customer = new Customer();
        var validationFailures = new List<ValidationFailure> { new ValidationFailure("Name", "Name cannot be empty") };
        var validationResult = new ValidationResult(validationFailures);
        _customerValidatorMock.Setup(x => x.Validate(customer)).Returns(validationResult);

        Assert.Throws<InvalidException>(() => _customerAdapter.Create(customer));
    }

    [Fact]
    public void Create_ShouldCallUseCaseCreate_WhenCustomerIsValid()
    {
        var customer = new Customer();
        var validationResult = new ValidationResult();
        _customerValidatorMock.Setup(x => x.Validate(customer)).Returns(validationResult);

        _customerAdapter.Create(customer);

        _customerUseCaseMock.Verify(x => x.Create(customer), Times.Once);
    }

    [Fact]
    public void Update_ShouldReturnUpdatedCustomer_WhenCustomerIsValid()
    {
        var customerId = Guid.NewGuid().ToString();
        var customer = new Customer { Id = customerId };
        _customerValidatorMock.Setup(x => x.Validate(customer)).Returns(new ValidationResult());
        _customerUseCaseMock.Setup(x => x.Update(customerId, customer)).Returns(customer);

        var result = _customerAdapter.Update(customerId, customer);

        Assert.Equal(customer, result);
    }

    [Fact]
    public void Delete_ShouldCallUseCaseDelete_WhenIdIsValid()
    {
        var customerId = Guid.NewGuid().ToString();

        _customerAdapter.Delete(customerId);

        _customerUseCaseMock.Verify(x => x.Delete(customerId), Times.Once);
    }
    
    [Fact]
    public void FindById_ShouldThrowInvalidIdFormatException_WhenIdIsInvalid()
    {
        var invalidId = "invalid-id";

        var exception = Assert.Throws<InvalidIdFormatException>(() => _customerAdapter.FindById(invalidId));
        Assert.Equal("Invalid ID format. ID must be a valid UUID.", exception.Message);
    }




}
