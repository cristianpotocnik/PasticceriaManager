using Pasticceria.Data;
using Pasticceria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Repository
{
    public interface IIngredientiRepository
    {
        Ingrediente GetById(Guid Id);
        List<Ingrediente> GetAll();
        Task Add(Ingrediente ingrediente);
        Task Remove(Guid id);
    }

    public class IngredintiRepository : IIngredientiRepository
    {
        private readonly ApplicationDbContext _context;
        public IngredintiRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Ingrediente GetById(Guid id)
        {
            if (id == null)
            {
                return null;
            }
            return _context.Ingredienti.Where(p => p.Id.Equals(id)).FirstOrDefault();
        }

        public List<Ingrediente> GetAll()
        {
            var ingredienti = new List<Ingrediente>();
            ingredienti = _context.Ingredienti.ToList();
            return ingredienti;
        }

        public async Task Add(Ingrediente ingrediente)
        {
            _context.Ingredienti.Add(ingrediente);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public async Task Remove(Guid id)
        {
            var ingrediente = _context.Ingredienti.Where(p => p.Id.Equals(id)).FirstOrDefault();
            if (ingrediente != null)
            {
                _context.Ingredienti.Remove(ingrediente);
            }
        }
    }
}
