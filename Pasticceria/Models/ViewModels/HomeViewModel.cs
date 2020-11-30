using Pasticceria.Models.Entities;
using Pasticceria.Models.ReportModel;
using Pasticceria.Models.ViewModels.Base;
using System.Collections.Generic;

namespace Pasticceria.Models.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<Articolo> Articoli { get; set; }
        public List<Ingrediente> Ingredienti { get; set; }
    }
}
