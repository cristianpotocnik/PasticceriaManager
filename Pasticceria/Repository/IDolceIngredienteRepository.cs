using Pasticceria.Data;
using Pasticceria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Repository
{
    public interface IDolceIngredienteRepository
    {
        List<DolceIngrediente> GetIngredientsByDolce(Guid dolceId);
        DolceIngrediente GetById(Guid dolceId, Guid ingredienteId);
        Task AssignIngrediente(DolceIngrediente dolceIngrediente);
        Task UnasignIngrediente(DolceIngrediente dolceIngrediente);
        Task RemoveAssignment(Guid dolceId);
    }
    public class DolceIngredienteRepository : IDolceIngredienteRepository
    {
        private readonly ApplicationDbContext _context;
        public DolceIngredienteRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<DolceIngrediente> GetIngredientsByDolce(Guid dolceId)
        {
            var dolceIngrediente = new List<DolceIngrediente>();
            if(dolceId==null) { return null; }
            foreach(var ingrediente in _context.DolceIngrediente.Where(p => p.DolceId.Equals(dolceId)).ToList())
            {
                dolceIngrediente.Add(ingrediente);
            }
            return dolceIngrediente;
        }

        public DolceIngrediente GetById(Guid dolceId, Guid ingredienteId)
        {
            var dolceIngrediente = new DolceIngrediente();
            if (dolceId == null) { return null; }
            dolceIngrediente = _context.DolceIngrediente.Where(p => p.DolceId.Equals(dolceId) && p.IngredienteId.Equals(ingredienteId)).FirstOrDefault();
            return dolceIngrediente;
        }

        public async Task AssignIngrediente(DolceIngrediente dolceIngrediente)
        {
            if (dolceIngrediente != null)
            {
                _context.Add(dolceIngrediente);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex) { }                
            }            
        }

        public async Task UnasignIngrediente(DolceIngrediente dolceIngrediente)
        {
            try
            {
                _context.DolceIngrediente.Remove(dolceIngrediente);
            }
            catch(Exception ex) { }
        }

        public async Task RemoveAssignment(Guid dolceId)
        {
            var assignments = new List<DolceIngrediente>();
            try
            {
                assignments = _context.DolceIngrediente.Where(p => p.DolceId.Equals(dolceId)).ToList();
                _context.RemoveRange(assignments);
            }
            catch (Exception ex) { }
        }
    }
}
