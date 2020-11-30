using Pasticceria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Models.ReportModel
{
    public class Articolo : Dolce
    {
        public List<Dose> Ingredienti { get; set; }
        public double Prezzo { get; set; }
    }
}
