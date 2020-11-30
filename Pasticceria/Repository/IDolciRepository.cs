using Pasticceria.Data;
using Pasticceria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Repository
{
    public interface IDolciRepository
    {
        Dolce GetById(Guid id);
        List<Dolce> GetAll();
        Task AddNew(Dolce dolce);
        Task Update(Dolce dolce);
        Task Remove(Guid id);
    }
    public class DolciRepository : IDolciRepository
    {
        private readonly ApplicationDbContext _context;
        public DolciRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Dolce GetById(Guid id)
        {
            var dolce = new Dolce();
            dolce = _context.Dolci.Where(p => p.Id.Equals(id)).FirstOrDefault();
            return dolce;
        }
        public List<Dolce> GetAll()
        {
            return _context.Dolci.ToList();
        }
        public async Task AddNew(Dolce dolce)
        {
            if (dolce != null)
            {
                _context.Add(dolce);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex) { }
        }
        public async Task Update(Dolce dolce)
        {
            var _dolce = _context.Dolci.Where(p => p.Id.Equals(dolce.Id)).FirstOrDefault();
            if (_dolce != null)
            {
                _dolce = dolce;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex) { }
                
            }
        }

        public async Task Remove(Guid id)
        {
            var _dolce = _context.Dolci.Where(p => p.Id.Equals(id)).FirstOrDefault();
            if (_dolce != null)
            {
                _context.Dolci.Remove(_dolce);
                _context.SaveChanges();
            }            
        }
    }
}
