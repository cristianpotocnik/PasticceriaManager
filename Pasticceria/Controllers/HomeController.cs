using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pasticceria.Controllers.Base;
using Pasticceria.Models;
using Pasticceria.Models.Entities;
using Pasticceria.Models.ReportModel;
using Pasticceria.Models.ViewModels;
using Pasticceria.Services;

namespace Pasticceria.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IArticoloService _articoloService;
        private HomeViewModel _homeView;

        public HomeController(
            IArticoloService articoloService)
        {
            _articoloService = articoloService ?? throw new ArgumentNullException(nameof(articoloService));
        }

        public IActionResult Index()
        {
            BindModel();
            return View(_homeView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articolo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AggiungiArticolo(Articolo articolo)
        {
            if (articolo.Name == null || articolo.UnitPrice==0)
            {
                return BadRequest();
            }
            await _articoloService.AddArticolo(articolo);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AggiungiIngrediente(Ingrediente ingrediente)
        {
            if (ingrediente.Name == null || ingrediente.Value==null)
            {
                return BadRequest();
            }
            await _articoloService.AddIngrediente(ingrediente);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dolceId"></param>
        /// <param name="ingredienteId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AssegnaIngrediente(Guid dolceId, Guid ingredienteId, double quantity)
        {
            await _articoloService.AddIngredientsDolce(dolceId, ingredienteId, quantity);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dolceId"></param>
        /// <param name="ingredienteId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TogliIngrediente(Guid dolceId, Guid ingredienteId)
        {
            await _articoloService.RemoveIngredientsDolce(dolceId, ingredienteId);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dolceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDolceDetails/{dolceId}")]
        public async Task<IActionResult> GetDolceDetails(Guid dolceId)
        {
            if (dolceId == null) { return BadRequest(); }
            var articolo = _articoloService.GetArticolo(dolceId);
            if (articolo == null) { return NotFound(); }
            return Ok(articolo);
        }

        [HttpGet]

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void BindModel()
        {            
            _homeView = new HomeViewModel();
            _homeView.Articoli = _articoloService.GetAll();
            _homeView.Ingredienti = _articoloService.GetIngredienti();
        }
    }
}
