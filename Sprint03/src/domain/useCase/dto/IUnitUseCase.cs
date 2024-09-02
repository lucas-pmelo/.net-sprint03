using Sprint03.domain.model;

namespace Sprint03.domain.useCase.dto;

public interface IUnitUseCase
{
    List<Unit> ListAll();
    Unit FindById(string id);
    void Create(Unit unit);
    Unit Update(string id, Unit unit);
    void Delete(string id);
}