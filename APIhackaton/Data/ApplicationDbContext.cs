using Microsoft.EntityFrameworkCore;
using APIhackaton.Models;

namespace APIhackaton.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for the models
        public DbSet<Alumno> Alumnos { get; set; } = null!;
        public DbSet<Docente> Docentes { get; set; } = null!;
        public DbSet<Tutor> Tutores { get; set; } = null!;
        public DbSet<Materia> Materias { get; set; } = null!;
        public DbSet<Curso> Cursos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here if needed
        }
    }
}
