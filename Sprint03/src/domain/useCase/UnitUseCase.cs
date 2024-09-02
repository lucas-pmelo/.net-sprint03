using Sprint03.infra.exception;
using Sprint03.domain.model;
using Sprint03.domain.repository;
using Sprint03.domain.useCase.dto;

namespace Sprint03.domain.useCase
{
    public class UnitUseCase : IUnitUseCase
    {
        private readonly IUnitRepository _unitRepository;

        public UnitUseCase(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public Unit FindById(string id)
        {
            var unit = _unitRepository.FindById(id);

            if (unit == null)
            {
                throw new NotFoundException($"Unit with ID {id} not found.");
            }

            return unit;
        }

        public List<Unit> ListAll()
        {
            return _unitRepository.ListAll();
        }

        public void Create(Unit unit)
        {
            var existingUnit = _unitRepository.FindById(unit.Id);

            if (existingUnit != null)
            {
                throw new AlreadyExistsException($"Unit with ID {unit.Id} already exists.");
            }

            _unitRepository.Create(unit);
        }

        public Unit Update(string id, Unit unit)
        {
            var existingUnit = _unitRepository.FindById(id);

            if (existingUnit == null)
            {
                throw new NotFoundException($"Unit with ID {id} not found.");
            }

            // Update assuming these properties are non-nullable
            if (!string.IsNullOrWhiteSpace(unit.Name))
            {
                existingUnit.Name = unit.Name;
            }
            if (!string.IsNullOrWhiteSpace(unit.Phone))
            {
                existingUnit.Phone = unit.Phone;
            }
            if (!string.IsNullOrWhiteSpace(unit.Email))
            {
                existingUnit.Email = unit.Email;
            }
            if (!string.IsNullOrWhiteSpace(unit.Type))
            {
                existingUnit.Type = unit.Type;
            }
            if (!string.IsNullOrWhiteSpace(unit.Cep))
            {
                existingUnit.Cep = unit.Cep;
            }

            _unitRepository.Update(existingUnit.Id, existingUnit);

            return existingUnit;
        }


        public void Delete(string id)
        {
            var unit = _unitRepository.FindById(id);

            if (unit == null)
            {
                throw new NotFoundException($"Unit with ID {id} not found.");
            }

            _unitRepository.Delete(id);
        }
    }
}
