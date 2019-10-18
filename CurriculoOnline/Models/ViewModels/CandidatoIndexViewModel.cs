using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Models.ViewModels
{
    public class CandidatoIndexViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public string Email { get; set; }

        public CandidatoIndexViewModel() { }
        public CandidatoIndexViewModel(int id, string nome, string dataNascimento, string nacionalidade, string email)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Nacionalidade = nacionalidade;
            Email = email;
        }
    }
}
