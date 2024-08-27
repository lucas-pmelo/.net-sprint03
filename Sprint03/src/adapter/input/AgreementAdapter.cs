using FluentValidation;
using Sprint03.domain.model;
using Sprint03.domain.useCase.dto;
using Sprint03.adapter.input.dto;

namespace Sprint03.adapter.input
{
    public class AgreementAdapter : IAgreementAdapter
    {
        private readonly IAgreementUseCase _agreementUseCase;
        private readonly IValidator<Agreement> _agreementValidator;

        public AgreementAdapter(IAgreementUseCase agreementUseCase, IValidator<Agreement> agreementValidator)
        {
            _agreementUseCase = agreementUseCase;
            _agreementValidator = agreementValidator;
        }

        public List<Agreement> ListAll()
        {
            return _agreementUseCase.ListAll();
        }

        public Agreement FindById(string id)
        {
            ValidateId(id);
            return _agreementUseCase.FindById(id);
        }

        public void Create(Agreement agreement)
        {
            ValidateAgreement(agreement);
            _agreementUseCase.Create(agreement);
        }

        public Agreement Update(string id, Agreement agreement)
        {
            ValidateId(id);
            ValidateAgreement(agreement);
            return _agreementUseCase.Update(id, agreement);
        }

        public void Delete(string id)
        {
            ValidateId(id);
            _agreementUseCase.Delete(id);
        }

        private void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            {
                throw new ArgumentException("Invalid ID format. ID must be a valid UUID.", nameof(id));
            }
        }

        private void ValidateAgreement(Agreement agreement)
        {
            var validationResult = _agreementValidator.Validate(agreement);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                );
            }
        }
    }
}
