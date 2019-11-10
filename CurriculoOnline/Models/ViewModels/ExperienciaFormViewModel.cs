using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Models.ViewModels
{
    public class ExperienciaFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Campo Profissao é obrigatório!")]
        public string Profissao { get; set; }

        [Required(ErrorMessage = "Campo Empresa é obrigatório!")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "Campo Data Inicio é obrigatório!")]
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        [Required(ErrorMessage = "Campo Cidade é obrigatório!")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Campo Estado é obrigatório!")]
        public int IdEstado { get; set; }

        public string Detalhes { get; set; }
    }
}
