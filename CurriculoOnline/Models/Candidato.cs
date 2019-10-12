using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Nacionalidade { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public Cidade Cidade { get; set; }
        public List<Formacao> Formacoes { get; set; } = new List<Formacao>();
        public List<Experiencia> Experiencias { get; set; } = new List<Experiencia>();

        public Candidato() { }

        public Candidato(int id, string nome, string nomePai, string nomeMae, string nacionalidade, DateTime dataNascimento, char sexo, string email, string telefone, string celular, Cidade cidade)
        {
            Id = id;
            Nome = nome;
            NomePai = nomePai;
            NomeMae = nomeMae;
            Nacionalidade = nacionalidade;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Email = email;
            Telefone = telefone;
            Celular = celular;
            Cidade = cidade;
        }
    }
}
