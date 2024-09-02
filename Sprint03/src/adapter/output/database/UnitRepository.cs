using System;
using System.Collections.Generic;
using System.Linq;
using Sprint03.infra.exception;
using Sprint03.domain.model;
using Sprint03.domain.repository;

namespace Sprint03.adapter.output.database
{
    public class UnitRepository : IUnitRepository
    {
        private static UnitRepository _instance;
        private static readonly object _lock = new object();
        private readonly ApplicationDbContext _context;

        // Construtor privado para evitar instanciação externa
        private UnitRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obter a instância única da classe
        public static UnitRepository GetInstance(ApplicationDbContext context)
        {
            if (_instance == null)
            {
                lock (_lock) // Garanta que a inicialização seja thread-safe
                {
                    if (_instance == null)
                    {
                        _instance = new UnitRepository(context);
                    }
                }
            }
            return _instance;
        }

        public List<Unit> ListAll()
        {
            return _context.Units.ToList();
        }

        public Unit FindById(string id)
        {
            return _context.Units.FirstOrDefault(a => a.Id == id);
        }

        public void Create(Unit unit)
        {
            _context.Units.Add(unit);
            _context.SaveChanges();
        }

        public Unit Update(string id, Unit unit)
        {
            var existingUnit = _context.Units.FirstOrDefault(a => a.Id == id);
            if (existingUnit == null)
            {
                throw new NotFoundException("Unit not found.");
            }

            existingUnit.Name = unit.Name;
            existingUnit.Phone = unit.Phone;
            existingUnit.Email = unit.Email;
            existingUnit.Type = unit.Type;
            existingUnit.Cep = unit.Cep;

            _context.Units.Update(existingUnit);
            _context.SaveChanges();

            return existingUnit;
        }

        public void Delete(string id)
        {
            var unit = _context.Units.FirstOrDefault(a => a.Id == id);
            if (unit == null)
            {
                throw new NotFoundException("Unit not found.");
            }

            _context.Units.Remove(unit);
            _context.SaveChanges();
        }
    }
}
