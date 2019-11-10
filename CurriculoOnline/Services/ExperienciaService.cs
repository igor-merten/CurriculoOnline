using CurriculoOnline.Data;
using CurriculoOnline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurriculoOnline.Services
{
    public class ExperienciaService
    {
        private readonly CurriculoOnlineContext _context;

        public ExperienciaService(CurriculoOnlineContext context)
        {
            _context = context;
        }

        public void Insert(Experiencia experiencia)
        {
            _context.Add(experiencia);
            _context.SaveChanges();
        }

        public bool Edit(Experiencia experiencia)
        {
            bool existe = _context.Experiencia.Any(e => e.Id == experiencia.Id);
            if (!existe)
                return false;

            _context.Update(experiencia);
            _context.SaveChanges();
            return true;
        }

        public List<Experiencia> FindByIdCandidato(int id)
        {
            return _context.Experiencia
                .Where(e => e.Candidato.Id == id)
                .Include(e => e.Cidade)
                .Include(e => e.Cidade.Estado)
                .ToList();
        }

        public Experiencia FindById(int id)
        {
            return _context.Experiencia
                .Where(e => e.Id == id)
                .Include(e => e.Cidade)
                .Include(e => e.Cidade.Estado)
                .FirstOrDefault();
        }

        public bool Delete(Experiencia experiencia)
        {
            bool existe = _context.Experiencia.Any(e => e.Id == experiencia.Id);
            if (!existe)
                return false;

            _context.Remove(experiencia);
            _context.SaveChanges();
            return true;
        }
    }
}
