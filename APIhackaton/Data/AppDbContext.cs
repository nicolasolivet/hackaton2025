using Microsoft.EntityFrameworkCore;
using APIhackaton.Models;

namespace APIhackaton.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; } = null!;
        public DbSet<Persona> Personas { get; set; } = null!;
        public DbSet<Curso> Cursos { get; set; } = null!;
        public DbSet<Docente> Docentes { get; set; } = null!;
        public DbSet<Materia> Materias { get; set; } = null!;
        public DbSet<Tutor> Tutores { get; set; } = null!;
        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<TutorAlumno> TutoresAlumnos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map Persona as base table (TPT) and derived types to their own tables
            modelBuilder.Entity<Persona>().ToTable("Personas");
            modelBuilder.Entity<Alumno>().ToTable("Alumnos");
            modelBuilder.Entity<Docente>().ToTable("Docentes");
            modelBuilder.Entity<Tutor>().ToTable("Tutores");

            // Performance: join entity between Alumno and Curso with extra fields
            modelBuilder.Entity<Performance>()
                .HasKey(p => new { p.AlumnoDni, p.CursoId });

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Alumno).WithMany(a => a.Performances)
                .HasForeignKey(p => p.AlumnoDni)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Curso).WithMany(c => c.Performances)
                .HasForeignKey(p => p.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // TutorAlumno: join table between Tutor and Alumno
            modelBuilder.Entity<TutorAlumno>()
                .HasKey(t => new { t.AlumnoDni, t.TutorDni });

            modelBuilder.Entity<TutorAlumno>()
                .HasOne(ta => ta.Alumno).WithMany(a => a.TutorAlumnos)
                .HasForeignKey(ta => ta.AlumnoDni)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TutorAlumno>()
                .HasOne(ta => ta.Tutor).WithMany(t => t.TutorAlumnos)
                .HasForeignKey(ta => ta.TutorDni)
                .OnDelete(DeleteBehavior.Cascade);

            // Docente - Curso
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Docente)
                .WithMany(d => d.Cursos)
                .HasForeignKey(c => c.dniDocente)
                .OnDelete(DeleteBehavior.SetNull);

            // Curso - Materia (FK: codigo -> Materia.codigo)
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Materia)
                .WithMany()
                .HasForeignKey(c => c.codigo)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed sample data
            modelBuilder.Entity<Materia>().HasData(
                new Materia { codigo = "MAT01", nombre = "Matemáticas" },
                new Materia { codigo = "FIS01", nombre = "Física" }
            );

            modelBuilder.Entity<Docente>().HasData(
                new Docente { dni = 1001, nombre = "Carlos", apellido = "Gomez", email = "carlos@gymail.com", rol = 2, estado = true },
                new Docente { dni = 1002, nombre = "Ana", apellido = "Perez", email = "ana@school.com", rol = 2, estado = true }
            );

            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 1, Anio = 2025, comision = 1, Turno = "Mañana", codigo = "MAT01", dniDocente = 1001 },
                new Curso { Id = 2, Anio = 2025, comision = 2, Turno = "Tarde", codigo = "FIS01", dniDocente = 1002 }
            );

            modelBuilder.Entity<Tutor>().HasData(
                new Tutor { dni = 2001, nombre = "Laura", apellido = "Ruiz", email = "laura@home.com", rol = 3, estado = true, ContadorAlertas = 0 }
            );

            modelBuilder.Entity<Alumno>().HasData(
                new Alumno { dni = 3001, nombre = "Juan", apellido = "Lopez", email = "juan@student.com", rol = 1, estado = true },
                new Alumno { dni = 3002, nombre = "María", apellido = "Garcia", email = "maria@student.com", rol = 1, estado = true }
            );

            modelBuilder.Entity<Performance>().HasData(
                new { AlumnoDni = 3001, CursoId = 1, Inasistencias = 2, ProgresoGeneral = 70, Racha = 3 },
                new { AlumnoDni = 3002, CursoId = 1, Inasistencias = 0, ProgresoGeneral = 85, Racha = 5 }
            );

            modelBuilder.Entity<TutorAlumno>().HasData(
                new { AlumnoDni = 3001, TutorDni = 2001 }
            );
        }
    }
}
