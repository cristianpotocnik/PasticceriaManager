using Pasticceria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Models.ReportModel
{
    public class Dose : Ingrediente
    {
        public Double Quantity { get; set; }
    }
}
