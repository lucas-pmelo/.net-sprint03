using Sprint03.domain.model;

namespace Sprint03.domain.repository;

public interface ICustomerRepository
{
    Customer FindById(string id);
    void Create(Customer customer);
    Customer Update(string id, Customer customer);
    void Delete(string id);
}