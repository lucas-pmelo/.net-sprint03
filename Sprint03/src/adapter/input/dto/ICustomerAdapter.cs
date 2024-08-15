using Sprint03.domain.model;

namespace Sprint03.adapter.input.dto;

public interface ICustomerAdapter
{
    Customer FindById(string id);
    void Create(Customer customer);
    Customer Update(string id, Customer customer);
    void Delete(string id);
}