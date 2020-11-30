using Microsoft.AspNetCore.Mvc;
using Pasticceria.Controllers.Base;

namespace Pasticceria.Controllers
{
    public class ManagementController : BaseController
    {
        public ManagementController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
