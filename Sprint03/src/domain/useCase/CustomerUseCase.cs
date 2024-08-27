using Sprint03.domain.exceptions;
using Sprint03.domain.model;
using Sprint03.domain.repository;
using Sprint03.domain.useCase.dto;

namespace Sprint03.domain.useCase
{
    public class CustomerUseCase : ICustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer FindById(string id)
        {
            var customer = _customerRepository.FindById(id);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer with ID {id} not found.");
            }

            return customer;
        }

        public void Create(Customer customer)
        {
            var persistedCustomer = _customerRepository.FindById(customer.Id);

            if (persistedCustomer != null)
            {
                throw new CustomerAlreadyExistsException($"Customer with ID {customer.Id} already exists.");
            }

            _customerRepository.Create(customer);
        }

        public Customer Update(string id, Customer customer)
        {
            var persistedCustomer = _customerRepository.FindById(id);

            if (persistedCustomer == null)
            {
                throw new CustomerNotFoundException($"Customer with ID {id} not found.");
            }

            _customerRepository.Update(id, customer);

            return customer;
        }

        public void Delete(string id)
        {
            var persistedCustomer = _customerRepository.FindById(id);

            if (persistedCustomer == null)
            {
                throw new CustomerNotFoundException($"Customer with ID {id} not found.");
            }

            _customerRepository.Delete(id);
        }
    }
}