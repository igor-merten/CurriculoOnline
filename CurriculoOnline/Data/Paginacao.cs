using System;
using System.ComponentModel.DataAnnotations;

namespace CurriculoOnline.Data
{
    public class Paginacao
    {
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
        public int TotalPaginas { get; set; }

        public Paginacao() {
            PaginaAtual = 0;
            ItensPorPagina = 5;
        }
        public Paginacao(int paginaAtual, int itensPorPagina)
        {
            
        }

        public void CalculaTotalPaginas(int totalItens)
        {
            double resultado = (double) totalItens / ItensPorPagina;
            TotalPaginas = resultado == (int)resultado ? (int)resultado-1 : (int) Math.Floor(resultado);
        }
    }
}
