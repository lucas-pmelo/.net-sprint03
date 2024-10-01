using Xunit;
using Moq;
using Sprint03.domain.model;
using Sprint03.domain.repository;
using Sprint03.domain.useCase;
using Sprint03.infra.exception;

public class CustomerUseCaseTest
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly CustomerUseCase _customerUseCase;

    public CustomerUseCaseTest()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _customerUseCase = new CustomerUseCase(_customerRepositoryMock.Object);
    }

    [Fact]
    public void FindById_ShouldReturnCustomer_WhenCustomerExists()
    {
        var customerId = Guid.NewGuid().ToString();
        var customer = new Customer { Id = customerId };
        _customerRepositoryMock.Setup(x => x.FindById(customerId)).Returns(customer);
        
        var result = _customerUseCase.FindById(customerId);

        Assert.Equal(customer, result);
    }

    [Fact]
    public void FindById_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        var customerId = Guid.NewGuid().ToString();
        _customerRepositoryMock.Setup(x => x.FindById(customerId)).Returns((Customer)null);

        Assert.Throws<NotFoundException>(() => _customerUseCase.FindById(customerId));
    }

    [Fact]
    public void Create_ShouldThrowAlreadyExistsException_WhenCustomerAlreadyExists()
    {
        var customer = new Customer { Id = Guid.NewGuid().ToString() };
        _customerRepositoryMock.Setup(x => x.FindById(customer.Id)).Returns(customer);

        Assert.Throws<AlreadyExistsException>(() => _customerUseCase.Create(customer));
    }

    [Fact]
    public void Create_ShouldCallRepositoryCreate_WhenCustomerDoesNotExist()
    {
        var customer = new Customer { Id = Guid.NewGuid().ToString() };
        _customerRepositoryMock.Setup(x => x.FindById(customer.Id)).Returns((Customer)null);
        
        _customerUseCase.Create(customer);

        _customerRepositoryMock.Verify(x => x.Create(customer), Times.Once);
    }

    [Fact]
    public void Update_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        var customerId = Guid.NewGuid().ToString();
        var customer = new Customer();
        _customerRepositoryMock.Setup(x => x.FindById(customerId)).Returns((Customer)null);

        Assert.Throws<NotFoundException>(() => _customerUseCase.Update(customerId, customer));
    }
    
    [Fact]
    public void Update_ShouldUpdateCustomer_WhenCustomerExists()
    {
        var customerId = Guid.NewGuid().ToString();
        var existingCustomer = new Customer { Id = customerId };
        var updatedCustomer = new Customer { Id = customerId, Name = "Updated Name" };

        _customerRepositoryMock.Setup(x => x.FindById(customerId)).Returns(existingCustomer);

        var result = _customerUseCase.Update(customerId, updatedCustomer);

        _customerRepositoryMock.Verify(x => x.Update(customerId, updatedCustomer), Times.Once);
        Assert.Equal(updatedCustomer, result);
    }

    
    [Fact]
    public void Delete_ShouldDeleteCustomer_WhenCustomerExists()
    {
        var customerId = "123";
        var existingCustomer = new Customer { Id = customerId };

        _customerRepositoryMock.Setup(repo => repo.FindById(customerId))
            .Returns(existingCustomer);

        _customerUseCase.Delete(customerId);

        _customerRepositoryMock.Verify(repo => repo.Delete(customerId), Times.Once);
    }

    [Fact]
    public void Delete_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        var customerId = "123";

        _customerRepositoryMock.Setup(repo => repo.FindById(customerId))
            .Returns((Customer)null);

        var exception = Assert.Throws<NotFoundException>(() => _customerUseCase.Delete(customerId));
        Assert.Equal($"Customer with ID {customerId} not found.", exception.Message);
    }
}
