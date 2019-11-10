using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurriculoOnline.Models;
using CurriculoOnline.Models.ViewModels;
using CurriculoOnline.Data;
using CurriculoOnline.Services;
using System.Dynamic;

namespace CurriculoOnline.Controllers
{
    public class ExperienciaController : Controller
    {
        private readonly ExperienciaService _experienciaService;
        private readonly EstadoService _estadoService;
        private readonly CidadeService _cidadeService;

        public ExperienciaController(EstadoService estadoService, CidadeService cidadeService, ExperienciaService experienciaService)
        {
            _estadoService = estadoService;
            _cidadeService = cidadeService;
            _experienciaService = experienciaService;
        }


        public IActionResult VerficaCampos(ExperienciaFormViewModel viewModel)
        {
            JsonResponse result = new JsonResponse();

            if (!ModelState.IsValid)
            {
                result.Texto = "Formulário preenchido de forma inválida.";
                return Json(result);
            }

            result.Texto = "Experiência salva com sucesso.";
            result.Sucesso = true;
            return Json(result);
        }

        public JsonResult ListaExperienciaCandidato(int idCandidato)
        {
            var experienciasbd = _experienciaService.FindByIdCandidato(idCandidato);

            dynamic experiencias = new List<dynamic>();

            foreach (var exp in experienciasbd)
            {
                dynamic experiencia = new ExpandoObject();

                var datafim = exp.DataFim.HasValue ? exp.DataFim.Value.ToString("dd/MM/yyyy") : "Atualmente";
                var data = exp.DataInicio.ToString("dd/MM/yyyy") + " - " + datafim;
                experiencia.Data = data;
                experiencia.Empresa = exp.Empresa;
                experiencia.Id = exp.Id;
                experiencia.Profissao = exp.Profissao;

                experiencias.Add(experiencia);
            }

            return Json(experiencias);
        }

        [HttpPost]
        public JsonResult Edit(ExperienciaFormViewModel viewModel)
        {
            if(!viewModel.Id.HasValue)
                return Json(new JsonResponse(false, "Id não encontrado para edição."));

            Experiencia experiencia = new Experiencia();

            experiencia.Id = viewModel.Id.Value;
            experiencia.Profissao = viewModel.Profissao;
            experiencia.Empresa = viewModel.Empresa;
            experiencia.DataFim = viewModel.DataFim;
            experiencia.DataInicio = viewModel.DataInicio;

            Cidade cidadeExperiencia = _cidadeService.FindById(viewModel.IdCidade);
            if (cidadeExperiencia == null)
                return Json(new JsonResponse(false, "Cidade não encontrada."));
            cidadeExperiencia.Estado = _estadoService.FindById(viewModel.IdEstado);

            experiencia.Cidade = cidadeExperiencia;
            experiencia.Detalhes = viewModel.Detalhes;

            var editaExperiencia = _experienciaService.Edit(experiencia);
            if (!editaExperiencia)
                return Json(new JsonResponse(false, "Impossível editar esta experiência. Por favor, entre em contato com a equipe de desenvolvimento."));

            return Json(new JsonResponse(true, "Experiência editada com sucesso!"));
        }

        public JsonResult BuscaPorId(int idCandidato)
        {
            Experiencia experienciabd = _experienciaService.FindById(idCandidato);

            dynamic experiencia = new ExpandoObject();

            experiencia.Id = experienciabd.Id;
            experiencia.Profissao = experienciabd.Profissao;
            experiencia.Empresa = experienciabd.Empresa;
            experiencia.DataInicio = experienciabd.DataInicio.ToString("yyyy-MM-dd");
            experiencia.DataFim = experienciabd.DataFim?.ToString("yyyy-MM-dd");
            experiencia.Detalhes = experienciabd.Detalhes;
            experiencia.IdCidade = experienciabd.Cidade.Id;
            experiencia.IdEstado = experienciabd.Cidade.Estado.Id;

            return Json(experiencia);
        }

        [HttpPost]
        public JsonResult Delete(int idExperiencia)
        {
            Experiencia experiencia = _experienciaService.FindById(idExperiencia);
            var deletaExperiencia = _experienciaService.Delete(experiencia);

            if (!deletaExperiencia)
                return Json(new JsonResponse(false, "Impossivel remover experiência: Id não encontrado."));

            return Json(new JsonResponse(true, "Experiência deletada com sucesso!"));
        }
    }
}
