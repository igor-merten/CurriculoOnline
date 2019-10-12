using Microsoft.EntityFrameworkCore;
using CurriculoOnline.Models;

namespace CurriculoOnline.Data
{
    public class CurriculoOnlineContext : DbContext
    {
        public CurriculoOnlineContext(DbContextOptions<CurriculoOnlineContext> options)
            : base(options)
        {
        }

        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Formacao> Formacao { get; set; }
        public DbSet<Experiencia> Experiencia { get; set; }

    }
}