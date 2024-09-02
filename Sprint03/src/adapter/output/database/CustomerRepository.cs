using System;
using System.Linq;
using Sprint03.domain.model;
using Sprint03.domain.repository;

namespace Sprint03.adapter.output.database
{
    public class CustomerRepository : ICustomerRepository
    {
        private static CustomerRepository _instance;
        private static readonly object _lock = new object();
        private readonly ApplicationDbContext _context;

        private CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public static CustomerRepository GetInstance(ApplicationDbContext context)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CustomerRepository(context);
                    }
                }
            }
            return _instance;
        }

        // Implementação dos métodos do repositório
        public Customer FindById(string id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer Update(string id, Customer customer)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.Id == id);

            existingCustomer.Name = customer.Name;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.BirthDate = customer.BirthDate;
            existingCustomer.Document = customer.Document;
            existingCustomer.Cep = customer.Cep;
            existingCustomer.AgreementId = customer.AgreementId;

            _context.Customers.Update(existingCustomer);
            _context.SaveChanges();

            return existingCustomer;
        }

        public void Delete(string id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
