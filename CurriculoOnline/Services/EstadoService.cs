using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CurriculoOnline.Data;
using CurriculoOnline.Models;

namespace CurriculoOnline.Services
{
    public class EstadoService
    {
        private readonly CurriculoOnlineContext _context;
        public EstadoService(CurriculoOnlineContext context)
        {
            _context = context;
        }

        public Estado FindById(int id)
        {
            return _context.Estado.Where(e => e.Id == id).SingleOrDefault();
        }

    }
}
