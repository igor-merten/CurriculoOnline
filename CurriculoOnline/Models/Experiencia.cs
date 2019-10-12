using System;

namespace CurriculoOnline.Models
{
    public class Experiencia
    {
        public int Id { get; set; }
        public string Profissao { get; set; }
        public string Empresa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Detalhes { get; set; }
        public Cidade Cidade { get; set; }
        public Candidato Candidato { get; set; }

        public Experiencia() { }

        public Experiencia(int id, string profissao, string empresa, DateTime dataInicio, DateTime dataFim, string detalhes, Cidade cidade, Candidato candidato)
        {
            Id = id;
            Profissao = profissao;
            Empresa = empresa;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Detalhes = detalhes;
            Cidade = cidade;
            Candidato = candidato;
        }
    }
}
