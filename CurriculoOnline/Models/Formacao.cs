using System;
using CurriculoOnline.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Models
{
    public class Formacao
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public string Instituicao { get; set; }
        public double CargaHoraria { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public StatusCurso Status { get; set; }
        public Cidade Cidade { get; set; }
        public Candidato Candidato { get; set; }

        public Formacao() { }

        public Formacao(int id, string curso, string instituicao, double cargaHoraria, DateTime dataInicio, DateTime dataFim, StatusCurso status, Cidade cidade, Candidato candidato)
        {
            Id = id;
            Curso = curso;
            Instituicao = instituicao;
            CargaHoraria = cargaHoraria;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Status = status;
            Cidade = cidade;
            Candidato = candidato;
        }
    }
}
