using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Models
{
    public class Candidato
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public string NomePai { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public string NomeMae { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public char Sexo { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public string Email { get; set; }

        public string Telefone { get; set; }
        public string Celular { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "{0} obrigatório!")]
        public Cidade Cidade { get; set; }

        public List<Formacao> Formacoes { get; set; } = new List<Formacao>();
        public List<Experiencia> Experiencias { get; set; } = new List<Experiencia>();

        public Candidato() { }

        public Candidato(int id, string nome, string nomePai, string nomeMae, string nacionalidade, DateTime dataNascimento, char sexo, string email, string telefone, string celular, string endereco, Cidade cidade)
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
            Endereco = endereco;
        }
    }
}
