using System.ComponentModel.DataAnnotations;

namespace APIhackaton.DTOs
{
    public class DocRefDto { public int dni { get; set; } }
    public class MateriaRefDto { public string codigo { get; set; } = string.Empty; }

    public class CursoCreateDto
    {
        public int Anio { get; set; }
        public int comision { get; set; }
        public string Turno { get; set; } = string.Empty;
        public string codigo { get; set; } = string.Empty;

        // Accept either a top-level dniDocente or a nested docente object { dni }
        public int? dniDocente { get; set; }
        public DocRefDto? docente { get; set; }

        // Optional nested materia reference
        public MateriaRefDto? materia { get; set; }
    }
}
