using System;
using Microsoft.EntityFrameworkCore;
using Sprint03.adapter.output.database;
using Sprint03.domain.model;
using Xunit;

public class CustomerRepositoryTests
{
    private ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Garante um banco de dados único para cada execução
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public void Create_AddsCustomerToDatabase()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var customer = new Customer
        {
            Id = "1",
            Name = "John Doe",
            Cep = "12345678",
            Document = "12345678900",
            Email = "john.doe@example.com",
            Phone = "1234567890",
            BirthDate = DateTime.UtcNow,
            AgreementId = 1
        };

        repository.Create(customer);

        var retrievedCustomer = context.Customers.Find("1");
        Assert.NotNull(retrievedCustomer);
        Assert.Equal("John Doe", retrievedCustomer.Name);
    }

    [Fact]
    public void Delete_RemovesCustomerFromDatabase()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var customer = new Customer
        {
            Id = "1",
            Name = "John Doe",
            Cep = "12345678",
            Document = "12345678900",
            Email = "john.doe@example.com",
            Phone = "1234567890",
            BirthDate = DateTime.UtcNow,
            AgreementId = 1
        };

        context.Customers.Add(customer);
        context.SaveChanges();

        repository.Delete("1");

        var retrievedCustomer = context.Customers.Find("1");
        Assert.Null(retrievedCustomer);
    }

    [Fact]
    public void Update_UpdatesCustomerInDatabase()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var customer = new Customer
        {
            Id = "1",
            Name = "John Doe",
            Cep = "12345678",
            Document = "12345678900",
            Email = "john.doe@example.com",
            Phone = "1234567890",
            BirthDate = DateTime.UtcNow,
            AgreementId = 1
        };

        context.Customers.Add(customer);
        context.SaveChanges();

        var updatedCustomer = new Customer
        {
            Id = "1",
            Name = "Jane Doe",
            Cep = "87654321",
            Document = "09876543211",
            Email = "jane.doe@example.com",
            Phone = "0987654321",
            BirthDate = customer.BirthDate,
            AgreementId = customer.AgreementId
        };

        var result = repository.Update("1", updatedCustomer);

        Assert.NotNull(result);
        Assert.Equal("Jane Doe", result.Name);
    }

    [Fact]
    public void FindById_ShouldReturnCustomer_WhenCustomerExists()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var customerId = "1";
        var customer = new Customer
        {
            Id = customerId,
            Name = "John Doe",
            Cep = "12345678",
            Document = "12345678900",
            Email = "john.doe@example.com",
            Phone = "1234567890",
            BirthDate = DateTime.UtcNow,
            AgreementId = 1
        };

        context.Customers.Add(customer);
        context.SaveChanges();

        var result = repository.FindById(customerId);

        Assert.NotNull(result);
        Assert.Equal(customerId, result.Id);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("12345678", result.Cep);
        Assert.Equal("12345678900", result.Document);
        Assert.Equal("john.doe@example.com", result.Email);
        Assert.Equal("1234567890", result.Phone);
    }


    [Fact]
    public void FindById_ShouldReturnNull_WhenCustomerDoesNotExist()
    {
        using var context = CreateContext();
        var repository = new CustomerRepository(context);

        var customerId = "1";

        var result = repository.FindById(customerId);

        Assert.Null(result);
    }
}
