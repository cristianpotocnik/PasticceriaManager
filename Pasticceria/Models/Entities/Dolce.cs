using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Models.Entities
{
    public class Dolce
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ProductionDate { get; set; }
        public double UnitPrice { get; set; }
        public bool IsValid { get; set; }
    }
}
