using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurriculoOnline.Services;
using CurriculoOnline.Models.ViewModels;
using CurriculoOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurriculoOnline.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly EstadoService _estadoService;
        private readonly CidadeService _cidadeService;

        public CandidatosController(EstadoService estadoService, CidadeService cidadeService)
        {
            _estadoService = estadoService;
            _cidadeService = cidadeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CandidatoFormViewModel { });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CandidatoFormViewModel viewModel)
        {
            return Json("Feshow");
        }

        public JsonResult ListaCidades(int idEstado)
        {
            var cidades = _cidadeService.FindByIdEstadoAsync(idEstado);
            return Json(cidades);
        }

        public JsonResult ListaEstados()
        {
            var estados = _estadoService.FindAll();
            return Json(estados);
        }
    }
}