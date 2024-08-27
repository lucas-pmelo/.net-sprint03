using Sprint03.domain.model;

namespace Sprint03.domain.repository;

public interface IAgreementRepository
{
    List<Agreement> ListAll();
    Agreement FindById(string id);
    void Create(Agreement agreement);
    Agreement Update(string id, Agreement agreement);
    void Delete(string id);
}