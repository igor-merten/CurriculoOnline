using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurriculoOnline.Services;
using CurriculoOnline.Models.ViewModels;
using CurriculoOnline.Models.Exceptions;
using CurriculoOnline.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CurriculoOnline.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Dynamic;

namespace CurriculoOnline.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly EstadoService _estadoService;
        private readonly CidadeService _cidadeService;
        private readonly CandidatoService _candidatoService;

        public CandidatosController(EstadoService estadoService, CidadeService cidadeService, CandidatoService candidatoService)
        {
            _estadoService = estadoService;
            _cidadeService = cidadeService;
            _candidatoService = candidatoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(CandidatoFormViewModel viewModel)
        {
            JsonResponse result = new JsonResponse();

            if (!ModelState.IsValid)
            {
                result.Texto = "Formulário preenchido de forma inválida.";
                return Json(result);
            }

            Cidade cidadeCandidato = _cidadeService.FindById(viewModel.IdCidade);
            if (cidadeCandidato == null)
                return Json(new JsonResponse(false, "Cidade não encontrada."));
            cidadeCandidato.Estado = _estadoService.FindById(viewModel.IdEstado);

            Candidato candidato = new Candidato();

            candidato.Nome = viewModel.Nome;
            candidato.DataNascimento = viewModel.DataNascimento.Date;
            candidato.Sexo = viewModel.Sexo;
            candidato.Nacionalidade = viewModel.Nacionalidade;
            candidato.NomeMae = viewModel.NomeMae;
            candidato.NomePai = viewModel.NomePai;
            candidato.Cidade = cidadeCandidato;
            candidato.Endereco = viewModel.NumEndereco.HasValue ?
                viewModel.Endereco + " " + viewModel.NumEndereco.Value.ToString() :
                viewModel.Endereco;
            candidato.Telefone = viewModel.Telefone;
            candidato.Celular = viewModel.Celular;
            candidato.Email = viewModel.Email;

            _candidatoService.Insert(candidato);

            result.Sucesso = true;
            result.Texto = "Candidato criado com sucesso!";
            return Json(result);
        }

        [HttpPost]
        public JsonResult Delete(int idCandidato)
        {
            idCandidato = 1000;
            Candidato candidato = _candidatoService.FindById(idCandidato);

            if (candidato == null)
                return Json(new JsonResponse(false, "Impossivel remover candidato: Id não encontrado."));

            return Json(new JsonResponse(true, "Candidato deletado com sucesso"));
        }

        public JsonResult ListaCidades(int idEstado)
        {
            var cidades = _cidadeService.FindByIdEstado(idEstado);
            return Json(cidades);
        }

        public JsonResult BuscaPorId(int idCandidato)
        {
            Candidato candidatobd = _candidatoService.FindById(idCandidato);

            dynamic candidato = new ExpandoObject();
            candidato.Id = candidatobd.Id;
            candidato.Nome = candidatobd.Nome;

            return Json(candidato);
        }

        public JsonResult ListaEstados()
        {
            var estados = _estadoService.FindAll();
            return Json(estados);
        }

        public JsonResult ListaCandidatos(Paginacao paginacao)
        {
            List<Candidato> candidatosbd = _candidatoService.FindAll();
            List<Candidato> cadidatosPagina = candidatosbd
                .Skip(paginacao.PaginaAtual * paginacao.ItensPorPagina)
                .Take(paginacao.ItensPorPagina)
                .ToList();

            List<CandidatoIndexViewModel> candidatos = new List<CandidatoIndexViewModel>();
            paginacao.CalculaTotalPaginas(candidatosbd.Count);

            foreach (Candidato c in cadidatosPagina)
            {
                candidatos.Add(new CandidatoIndexViewModel(c.Id, c.Nome, c.DataNascimento.ToString("dd/MM/yyyy"), c.Nacionalidade, c.Email));
            };

            dynamic foo = new ExpandoObject();

            foo.candidatos = candidatos;
            foo.paginacao = paginacao;

            return Json(foo);

        }
    }
}