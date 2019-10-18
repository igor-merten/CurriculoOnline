using CurriculoOnline.Data;
using CurriculoOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurriculoOnline.Services
{
    public class CidadeService
    {
        private readonly CurriculoOnlineContext _context;

        public CidadeService(CurriculoOnlineContext context)
        {
            _context = context;
        }

        //public async Task<List<Cidade>> FindByEstadoAsync(Estado estado)
        //{
        //    if (estado != null)
        //        return await _context.Cidade.Where(c => c.Estado == estado).OrderBy(c => c.Nome).DefaultIfEmpty().ToListAsync();
        //    else
        //        return null;
        //}

        public List<Cidade> FindByIdEstado(int idEstado)
        {
             return _context.Cidade.Where(c => c.Estado.Id == idEstado).OrderBy(c => c.Nome).DefaultIfEmpty().ToList();
        }

        public Cidade FindById(int id)
        {
            return _context.Cidade.Where(c => c.Id == id).FirstOrDefault();
        }

    }
}
