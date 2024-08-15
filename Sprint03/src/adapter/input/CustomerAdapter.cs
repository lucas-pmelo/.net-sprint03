using Sprint03.adapter.input.dto;
using Sprint03.domain.model;
using Sprint03.domain.useCase.dto;

namespace Sprint03.adapter.input;

public class CustomerAdapter : ICustomerAdapter
{
    private readonly ICustomerUseCase _customerUseCase;

    public CustomerAdapter(ICustomerUseCase customerUseCase)
    {
        _customerUseCase = customerUseCase;
    }

    public Customer FindById(string id)
    {
        ValidateId(id);
        return _customerUseCase.FindById(id);
    }

    public void Create(Customer customer)
    {
        ValidateCustomer(customer);
        _customerUseCase.Create(customer);
    }

    public Customer Update(string id, Customer customer)
    {
        ValidateId(id);
        ValidateCustomer(customer);
        return _customerUseCase.Update(id, customer);
    }

    public void Delete(string id)
    {
        ValidateId(id);
        _customerUseCase.Delete(id);
    }

    private void ValidateId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("ID cannot be null or empty.", nameof(id));
        }

        if (!Guid.TryParse(id, out _))
        {
            throw new ArgumentException("Invalid ID format. ID must be a valid UUID.", nameof(id));
        }
    }

    private void ValidateCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        if (string.IsNullOrWhiteSpace(customer.Name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(customer.Name));
        }

        if (string.IsNullOrWhiteSpace(customer.Document))
        {
            throw new ArgumentException("Document cannot be null or empty.", nameof(customer.Document));
        }

        if (string.IsNullOrWhiteSpace(customer.Cep))
        {
            throw new ArgumentException("CEP cannot be null or empty.", nameof(customer.Cep));
        }

        if (customer.BirthDate == DateTime.MinValue)
        {
            throw new ArgumentException("BirthDate cannot be null or empty.", nameof(customer.BirthDate));
        }

        if (customer.AgreementId <= 0)
        {
            throw new ArgumentException("AgreementId must be greater than zero.", nameof(customer.AgreementId));
        }
    }
}
