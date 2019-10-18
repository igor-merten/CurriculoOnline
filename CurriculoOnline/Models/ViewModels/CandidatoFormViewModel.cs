using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace CurriculoOnline.Models.ViewModels
{
    public class CandidatoFormViewModel
    {

        [Required(ErrorMessage = "Campo Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Nome do Pai é obrigatório!")]
        public string NomePai { get; set; }

        [Required(ErrorMessage = "Campo Nome da Mãe é obrigatório!")]
        public string NomeMae { get; set; }

        [Required(ErrorMessage = "Campo Nacionalidade é obrigatório!")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "Campo Nascimento é obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo Sexo é obrigatório!")]
        public char Sexo { get; set; }

        [Required(ErrorMessage = "Campo Email é obrigatório!")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        [DisplayFormat(DataFormatString = "{0:(99) 99999-9999")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Campo Endereço é obrigatório!")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Campo Cidade é obrigatório!")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Campo Estado é obrigatório!")]
        public int IdEstado { get; set; }

        public int? NumEndereco { get; set; }

    }
}
