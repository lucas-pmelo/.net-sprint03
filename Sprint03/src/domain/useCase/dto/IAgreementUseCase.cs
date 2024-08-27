using Sprint03.domain.model;

namespace Sprint03.domain.useCase.dto;

public interface IAgreementUseCase
{
    List<Agreement> ListAll();
    Agreement FindById(string id);
    void Create(Agreement agreement);
    Agreement Update(string id, Agreement agreement);
    void Delete(string id);
}