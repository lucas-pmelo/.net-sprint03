using Sprint03.domain.model;

namespace Sprint03.adapter.input.dto;

public interface IAgreementAdapter
{
    List<Agreement> ListAll();
    Agreement FindById(string id);
    void Create(Agreement agreement);
    Agreement Update(string id, Agreement agreement);
    void Delete(string id);
}