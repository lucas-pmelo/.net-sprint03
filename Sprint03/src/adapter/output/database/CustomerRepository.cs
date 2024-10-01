using Sprint03.domain.model;
using Sprint03.domain.repository;

namespace Sprint03.adapter.output.database
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

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

            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Email = customer.Email;
                existingCustomer.BirthDate = customer.BirthDate;
                existingCustomer.Document = customer.Document;
                existingCustomer.Cep = customer.Cep;
                existingCustomer.AgreementId = customer.AgreementId;

                _context.Customers.Update(existingCustomer);
                _context.SaveChanges();
            }

            return existingCustomer;
        }

        public void Delete(string id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}