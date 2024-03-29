﻿using System;
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

        public List<Estado> FindAllAsync()
        {
            return _context.Estado.OrderBy(e => e.Nome).ToList();
        }

        public List<Estado> FindAll()
        {
            return _context.Estado.OrderBy(e => e.Nome).ToList();
        }

    }
}
