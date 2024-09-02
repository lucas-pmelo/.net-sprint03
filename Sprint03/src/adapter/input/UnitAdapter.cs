using FluentValidation;
using Sprint03.domain.model;
using Sprint03.domain.useCase.dto;
using Sprint03.adapter.input.dto;

namespace Sprint03.adapter.input
{
    public class UnitAdapter : IUnitAdapter
    {
        private readonly IUnitUseCase _unitUseCase;
        private readonly IValidator<Unit> _unitValidator;

        public UnitAdapter(IUnitUseCase unitUseCase, IValidator<Unit> unitValidator)
        {
            _unitUseCase = unitUseCase;
            _unitValidator = unitValidator;
        }

        public List<Unit> ListAll()
        {
            return _unitUseCase.ListAll();
        }

        public Unit FindById(string id)
        {
            ValidateId(id);
            return _unitUseCase.FindById(id);
        }

        public void Create(Unit unit)
        {
            ValidateUnit(unit);
            _unitUseCase.Create(unit);
        }

        public Unit Update(string id, Unit unit)
        {
            ValidateId(id);
            ValidateUnit(unit);
            return _unitUseCase.Update(id, unit);
        }

        public void Delete(string id)
        {
            ValidateId(id);
            _unitUseCase.Delete(id);
        }

        private void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            {
                throw new ArgumentException("Invalid ID format. ID must be a valid UUID.", nameof(id));
            }
        }

        private void ValidateUnit(Unit unit)
        {
            var validationResult = _unitValidator.Validate(unit);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                );
            }
        }
    }
}
