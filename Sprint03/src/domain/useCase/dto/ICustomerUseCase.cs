using Sprint03.domain.model;

namespace Sprint03.domain.useCase.dto;

public interface ICustomerUseCase
{
    Customer FindById(string id);
    void Create(Customer customer);
    Customer Update(string id, Customer customer);
    void Delete(string id);
}