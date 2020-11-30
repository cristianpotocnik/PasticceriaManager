using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Models.Entities
{
    public class DolceIngrediente
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DolceId { get; set; }
        public Guid IngredienteId { get; set; }
        public double Quantity { get; set; }
    }
}
