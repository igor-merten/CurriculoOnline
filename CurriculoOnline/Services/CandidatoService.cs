using CurriculoOnline.Data;
using CurriculoOnline.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<Candidato> FindAll()
        {
            return _context.Candidato.OrderBy(c => c.Nome).ToList();
        }

        public Candidato FindById(int IdCandidato)
        {
            return _context.Candidato.Where(c => c.Id == IdCandidato).FirstOrDefault();
        }

        public void Insert(Candidato candidato)
        {
            _context.Add(candidato);
            _context.SaveChanges();
        }
    }
}
