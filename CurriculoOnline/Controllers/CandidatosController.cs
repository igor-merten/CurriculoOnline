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
        public JsonResult CreateOrEdit(CandidatoFormViewModel viewModel)
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
            candidato.DataNascimento = viewModel.DataNascimento;
            candidato.Sexo = viewModel.Sexo;
            candidato.Nacionalidade = viewModel.Nacionalidade;
            candidato.NomeMae = viewModel.NomeMae;
            candidato.NomePai = viewModel.NomePai;
            candidato.Cidade = cidadeCandidato;
            candidato.Endereco = viewModel.Endereco;
            candidato.Telefone = viewModel.Telefone;
            candidato.Celular = viewModel.Celular;
            candidato.Email = viewModel.Email;

            foreach (var exp in viewModel.Experiencias)
            {
                Experiencia experiencia = new Experiencia();
                experiencia.Candidato = candidato;
                experiencia.Cidade = _cidadeService.FindById(exp.IdCidade);
                if (experiencia.Cidade == null)
                    return Json(new JsonResponse(false, "Cidade não encontrada."));
                experiencia.DataFim = exp.DataFim;
                experiencia.DataInicio = exp.DataInicio;
                experiencia.Detalhes = exp.Detalhes;
                experiencia.Empresa = exp.Empresa;
                experiencia.Profissao = exp.Profissao;

                candidato.Experiencias.Add(experiencia);
            }

            if (viewModel.Id.HasValue)
            {
                candidato.Id = viewModel.Id.Value;
                var editaCandidato = _candidatoService.Edit(candidato);
                if(!editaCandidato)
                    return Json(new JsonResponse(false, "Impossível editar este candidato: Id não encontrado."));
                return Json(new JsonResponse(true, "Candidato editado com sucesso!"));
            }

            _candidatoService.Insert(candidato);

            result.Sucesso = true;
            result.Texto = "Candidato criado com sucesso!";
            return Json(result);
        }

        [HttpPost]
        public JsonResult Delete(int idCandidato)
        {
            Candidato candidato = _candidatoService.FindById(idCandidato);
            var deletaCandidato = _candidatoService.Delete(candidato);

            if (!deletaCandidato)
                return Json(new JsonResponse(false, "Impossivel remover candidato: Id não encontrado."));

            return Json(new JsonResponse(true, "Candidato deletado com sucesso!"));
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
            candidato.DataNascimento = candidatobd.DataNascimento.ToString("yyyy-MM-dd");
            candidato.Sexo = candidatobd.Sexo;
            candidato.Nacionalidade = candidatobd.Nacionalidade;
            candidato.NomeMae = candidatobd.NomeMae;
            candidato.NomePai = candidatobd.NomePai;
            candidato.Endereco = candidatobd.Endereco;
            candidato.Email = candidatobd.Email;
            candidato.Celular = candidatobd.Celular;
            candidato.Telefone = candidatobd.Telefone;
            candidato.IdCidade = candidatobd.Cidade.Id;
            candidato.IdEstado = candidatobd.Cidade.Estado.Id;

            var experiencias = new List<dynamic>();

            foreach(var exp in candidatobd.Experiencias)
            {
                dynamic experiencia = new ExpandoObject();

                experiencia.Cidade = exp.Cidade;
                experiencia.DataFim = exp.DataFim;
                experiencia.DataInicio = exp.DataInicio;
                var datafim = exp.DataFim.HasValue ? exp.DataFim.Value.ToString("dd/MM/yyyy") : "Atualmente";
                var data = exp.DataInicio.ToString("dd/MM/yyyy") + " - " + datafim;
                experiencia.Data = data;
                experiencia.Detalhes = exp.Detalhes;
                experiencia.Empresa = exp.Empresa;
                experiencia.Id = exp.Id;
                experiencia.Profissao = exp.Profissao;

                experiencias.Add(experiencia);
            }

            candidato.Experiencias = experiencias;

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