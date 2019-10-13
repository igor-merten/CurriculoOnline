using System.Collections.Generic;

namespace CurriculoOnline.Models.ViewModels
{
    public class CandidatoFormViewModel
    {
        public Candidato Candidato { get; set; }
        public int NumEndereco { get; set; }
        public ICollection<Estado> Estados { get; set; }
        public Cidade cidade { get; set; }
    }
}
