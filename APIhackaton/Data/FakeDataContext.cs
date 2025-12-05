// APIhackaton/Data/FakeDataContext.cs
using System;
using System.Collections.Generic;
using System.Linq;
using APIhackaton.Models;

namespace APIhackaton.Data
{
    public static class FakeDataContext
    {
        // colecciones públicas in-memory
        public static List<Alumno> Alumnos { get; } = new();
        public static List<Materia> Materias { get; } = new();
        public static List<Nota> Notas { get; } = new();

        // sincronización para id autoincremental
        private static readonly object _lock = new();
        private static int _nextNotaId = 1;

        static FakeDataContext()
        {
            // Alumnos de ejemplo (usa tu constructor)
            var a1 = new Alumno(12345678, "Juan", "Pérez", "juan@test.com", 1, true);
            a1.dni = 12345678;
            Alumnos.Add(a1);

            var a2 = new Alumno(87654321, "Ana", "García", "ana@test.com", 1, true);
            a2.dni = 87654321;
            Alumnos.Add(a2);

            // Materias (usa codigo como string)
            Materias.Add(new Materia("MAT01", "Matemática"));
            Materias.Add(new Materia("LEN01", "Lengua"));

            // Notas de ejemplo
            AddNota(new Nota { dniAlumno = 12345678, idMateria = "MAT01", valor = 4.0, fecha = DateTime.UtcNow.AddDays(-7) });
            AddNota(new Nota { dniAlumno = 12345678, idMateria = "MAT01", valor = 5.0, fecha = DateTime.UtcNow.AddDays(-1) });
            AddNota(new Nota { dniAlumno = 87654321, idMateria = "LEN01", valor = 8.5, fecha = DateTime.UtcNow.AddDays(-2) });
        }

        public static Nota AddNota(Nota nota)
        {
            lock (_lock)
            {
                nota.idNota = _nextNotaId++;
                Notas.Add(nota);
            }
            return nota;
        }

        // util: buscar materia por codigo
        public static Materia? GetMateriaByCodigo(string codigo) =>
            Materias.FirstOrDefault(m => string.Equals(m.codigo, codigo, StringComparison.OrdinalIgnoreCase));
    }
}
