using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace CurriculoOnline.Models.ViewModels
{
    public class CandidatoFormViewModel
    {

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nome do Pai é obrigatório!")]
        public string NomePai { get; set; }

        [Required(ErrorMessage = "Nome da Mãe é obrigatório!")]
        public string NomeMae { get; set; }

        [Required(ErrorMessage = "Nacionalidade é obrigatório!")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "Nascimento é obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatório!")]
        public char Sexo { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        [DisplayFormat(DataFormatString = "{0:(99) 99999-9999")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório!")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatório!")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório!")]
        public int IdEstado { get; set; }

        public int NumEndereco { get; set; }

    }
}
