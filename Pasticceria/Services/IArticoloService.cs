using Pasticceria.Models.Entities;
using Pasticceria.Models.ReportModel;
using Pasticceria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Services
{
    public interface IArticoloService
    {
        Articolo GetArticolo(Guid dolceId);
        List<Articolo> GetAll();
        List<Ingrediente> GetIngredienti();
        Task AddArticolo(Articolo articolo);
        Task UpdateArticoloName(Articolo articolo);        
        Task RemoveArticolo(Guid id);
        Task AddIngrediente(Ingrediente ingrediente);
        Task RemoveIngrediente(Guid id);
        Task AddIngredientsDolce(Guid dolceId, Guid ingredienteId, double quantity);
        Task RemoveIngredientsDolce(Guid dolceId, Guid ingredienteId);
        
    }

    public class ArticoloService : IArticoloService
    {
        private readonly IDolciRepository _dolciRepository;
        private readonly IIngredientiRepository _ingredientiRepository;
        private readonly IDolceIngredienteRepository _dolceIngredienteRepository;
        public ArticoloService(
            IDolciRepository dolciRepository,
            IIngredientiRepository ingredientiRepository,
            IDolceIngredienteRepository dolceIngredienteRepository)
        {
            _dolciRepository = dolciRepository ?? throw new ArgumentNullException(nameof(dolciRepository));
            _ingredientiRepository = ingredientiRepository ?? throw new ArgumentNullException(nameof(ingredientiRepository));
            _dolceIngredienteRepository = dolceIngredienteRepository ?? throw new ArgumentNullException(nameof(dolceIngredienteRepository));
        }

        public Articolo GetArticolo(Guid articoloId)
        {
            var dolce = _dolciRepository.GetById(articoloId);
            var ingredienti = new List<Dose>();
            foreach (var item in _dolceIngredienteRepository.GetIngredientsByDolce(dolce.Id))
            {
                var ingrediente = _ingredientiRepository.GetById(item.IngredienteId);
                ingredienti.Add(new Dose
                {
                    Name = ingrediente.Name,
                    Quantity = item.Quantity,
                    Value = ingrediente.Value
                });
            }

            var articolo  = new Articolo
            {
                Name = dolce.Name,
                Ingredienti = ingredienti,
                ProductionDate = dolce.ProductionDate,
                Id = dolce.Id,
                UnitPrice = dolce.UnitPrice,
                Prezzo = GetUpdatedPrice(dolce),
                IsValid = (GetUpdatedPrice(dolce) != 0) ? true : false
            };
            return articolo;
        }

        public List<Articolo> GetAll()
        {
            var articoli = new List<Articolo>();
            foreach(var dolce in _dolciRepository.GetAll().OrderByDescending(o=>o.ProductionDate))
            {
                var ingredienti = new List<Dose>();
                foreach(var item in _dolceIngredienteRepository.GetIngredientsByDolce(dolce.Id))
                {
                    var ingrediente = _ingredientiRepository.GetById(item.IngredienteId);
                    ingredienti.Add(new Dose
                    {
                        Name = ingrediente.Name,
                        Quantity = item.Quantity,
                        Value = ingrediente.Value
                    });
                }
                
                articoli.Add(new Articolo
                {
                    Name = dolce.Name,
                    Ingredienti = ingredienti,
                    ProductionDate = dolce.ProductionDate,
                    Id = dolce.Id,
                    UnitPrice = dolce.UnitPrice,
                    Prezzo = GetUpdatedPrice(dolce),
                    IsValid = (GetUpdatedPrice(dolce)!=0) ? true : false
                });
            }
            return articoli;
        }

        public async Task AddArticolo(Articolo articolo)
        {
            if (articolo != null)
            {
                var dolce = new Dolce
                {
                    Id = Guid.NewGuid(),
                    Name = articolo.Name,
                    UnitPrice = articolo.UnitPrice,
                    IsValid = true,
                    ProductionDate = DateTime.Now
                };
                await _dolciRepository.AddNew(dolce);
                //foreach (var ingrediente in articolo.Ingredienti)
                //{
                //    await _dolceIngredienteRepository.AssignIngrediente(new DolceIngrediente
                //    {
                //        Id = Guid.NewGuid(),
                //        DolceId = dolce.Id,
                //        IngredienteId = ingrediente.Id,
                //        Quantity = ingrediente.Quantity
                //    });
                //}
            }
        }

        public async Task UpdateArticoloName(Articolo articolo)
        {            
            if(articolo!=null) 
            {
                var dolce = _dolciRepository.GetById(articolo.Id);
                if (dolce !=null) 
                {
                    dolce.Name = articolo.Name;
                    await _dolciRepository.Update(dolce);
                }               
            }                   
        }

        public async Task RemoveArticolo(Guid id)
        {
            await _dolciRepository.Remove(id);
            await _dolceIngredienteRepository.RemoveAssignment(id);
        }

        public List<Ingrediente> GetIngredienti()
        {
            return _ingredientiRepository.GetAll();
        }

        public async Task AddIngrediente(Ingrediente ingrediente)
        {
            if (ingrediente != null)
            {
                ingrediente.Id = Guid.NewGuid();
                await _ingredientiRepository.Add(ingrediente);
            }            
        }

        public async Task RemoveIngrediente(Guid id)
        {
            await _ingredientiRepository.Remove(id);
        }

        public async Task AddIngredientsDolce(Guid dolceId, Guid ingredienteId, double quantity)
        {
            var dolceIngrediente = new DolceIngrediente()
            {
                Id = Guid.NewGuid(),
                DolceId = dolceId,
                IngredienteId = ingredienteId,
                Quantity = quantity
            };
            await _dolceIngredienteRepository.AssignIngrediente(dolceIngrediente);
        }
        public async Task RemoveIngredientsDolce(Guid dolceId, Guid ingredienteId)
        {
            var assignment = _dolceIngredienteRepository.GetById(dolceId, ingredienteId);
            if (assignment != null)
            {
                await _dolceIngredienteRepository.UnasignIngrediente(assignment);
            }
        }

        public double GetUpdatedPrice(Dolce dolce)
        {
            var days = (DateTime.Now - dolce.ProductionDate).Days;
            switch (days)
            {
                case 0:
                    return dolce.UnitPrice;
                case 1: 
                    return dolce.UnitPrice * 0.8;
                case 2:
                    return dolce.UnitPrice * 0.2;
                default:
                    return 0;
            }
        }
    }
}
