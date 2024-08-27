using Sprint03.domain.model;
using Sprint03.domain.repository;
using Sprint03.infra.exception;
using Sprint03.domain.useCase.dto;

namespace Sprint03.domain.useCase
{
    public class AgreementUseCase : IAgreementUseCase
    {
        private readonly IAgreementRepository _agreementRepository;

        public AgreementUseCase(IAgreementRepository agreementRepository)
        {
            _agreementRepository = agreementRepository;
        }

        public Agreement FindById(string id)
        {
            var agreement = _agreementRepository.FindById(id);

            if (agreement == null)
            {
                throw new NotFoundException("Agreement not found.");
            }

            return agreement;
        }

        public List<Agreement> ListAll()
        {
            return _agreementRepository.ListAll();
        }

        public void Create(Agreement agreement)
        {
            var existingAgreement = _agreementRepository.FindById(agreement.Id);

            if (existingAgreement != null)
            {
                throw new InvalidOperationException("Agreement with the same ID already exists.");
            }

            _agreementRepository.Create(agreement);
        }

        public Agreement Update(string id, Agreement agreement)
        {
            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            {
                throw new InvalidIdFormatException("Invalid ID format. ID must be a valid UUID.");
            }

            var existingAgreement = _agreementRepository.FindById(id);
            if (existingAgreement == null)
            {
                throw new NotFoundException("Agreement not found.");
            }

            existingAgreement.Name = agreement.Name ?? existingAgreement.Name; 
            existingAgreement.Value = agreement.Value;
            existingAgreement.ServiceType = agreement.ServiceType ?? existingAgreement.ServiceType;
            existingAgreement.Coverage = agreement.Coverage ?? existingAgreement.Coverage;

            _agreementRepository.Update(existingAgreement.Id, existingAgreement);

            return existingAgreement;
        }


        public void Delete(string id)
        {
            var agreement = _agreementRepository.FindById(id);

            if (agreement == null)
            {
                throw new NotFoundException("Agreement not found.");
            }

            _agreementRepository.Delete(id);
        }
    }
}
