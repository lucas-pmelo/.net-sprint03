using Sprint03.infra.exception;
using Sprint03.domain.model;
using Sprint03.domain.repository;

namespace Sprint03.adapter.output.database
{
    public class AgreementRepository : IAgreementRepository
    {
        private readonly ApplicationDbContext _context;

        // Construtor público que aceita o ApplicationDbContext
        public AgreementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<Agreement> ListAll()
        {
            return _context.Agreements.ToList();
        }

        public Agreement FindById(string id)
        {
            return _context.Agreements.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Agreement agreement)
        {
            _context.Agreements.Add(agreement);
            _context.SaveChanges();
        }

        public Agreement Update(string id, Agreement agreement)
        {
            var existingAgreement = _context.Agreements.FirstOrDefault(a => a.Id == id);
            if (existingAgreement == null)
            {
                throw new NotFoundException("Agreement not found.");
            }

            existingAgreement.Name = agreement.Name;
            existingAgreement.Value = agreement.Value;
            existingAgreement.ServiceType = agreement.ServiceType;
            existingAgreement.Coverage = agreement.Coverage;

            _context.Agreements.Update(existingAgreement);
            _context.SaveChanges();

            return existingAgreement;
        }

        public void Delete(string id)
        {
            var agreement = _context.Agreements.FirstOrDefault(a => a.Id == id);
            if (agreement == null)
            {
                throw new NotFoundException("Agreement not found.");
            }

            _context.Agreements.Remove(agreement);
            _context.SaveChanges();
        }
    }
}