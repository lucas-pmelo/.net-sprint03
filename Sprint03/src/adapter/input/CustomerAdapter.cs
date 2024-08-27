using FluentValidation;
using Sprint03.domain.model;
using Sprint03.domain.useCase.dto;
using System.Linq;
using Sprint03.adapter.input.dto;

namespace Sprint03.adapter.input
{
    public class CustomerAdapter : ICustomerAdapter
    {
        private readonly ICustomerUseCase _customerUseCase;
        private readonly IValidator<Customer> _customerValidator;

        public CustomerAdapter(ICustomerUseCase customerUseCase, IValidator<Customer> customerValidator)
        {
            _customerUseCase = customerUseCase;
            _customerValidator = customerValidator;
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
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            {
                throw new ArgumentException("Invalid ID format. ID must be a valid UUID.", nameof(id));
            }
        }

        private void ValidateCustomer(Customer customer)
        {
            var validationResult = _customerValidator.Validate(customer);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                );
            }
        }
    }
}