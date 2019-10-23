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
            return _context.Candidato
                .Where(c => c.Id == IdCandidato)
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .FirstOrDefault();
        }

        public void Insert(Candidato candidato)
        {
            _context.Add(candidato);
            _context.SaveChanges();
        }

        public bool Edit(Candidato candidato)
        {
            bool existe = _context.Candidato.Any(c => c.Id == candidato.Id);
            if (!existe)
                return false;

            _context.Update(candidato);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Candidato candidato)
        {
            bool existe = _context.Candidato.Any(c => c.Id == candidato.Id);
            if (!existe)
                return false;

            _context.Remove(candidato);
            _context.SaveChanges();
            return true;
        }

        public async Task EditAsync(Candidato candidato)
        {
            _context.Update(candidato);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidato> FindByIdAsync(int IdCandidato)
        {
            return await _context.Candidato
                .Where(c => c.Id == IdCandidato)
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .FirstOrDefaultAsync();
        }

    }
}
