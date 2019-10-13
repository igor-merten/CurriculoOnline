using CurriculoOnline.Data;
using CurriculoOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Services
{
    public class CandidatoService
    {
        private readonly CurriculoOnlineContext _context;
        public CandidatoService(CurriculoOnlineContext context)
        {
            _context = context;
        }

        public async void InserirAsync(Candidato candidato)
        {
            _context.Add(candidato);
            await _context.SaveChangesAsync();
        }
    }
}
