using Pasticceria.Models.ReportModel;
using Pasticceria.Models.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pasticceria.Models.ViewModels
{
    public class ManagementViewModel : BaseViewModel
    {
        public List<Articolo> Articoli { get; set; }
    }
}
