using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIhackaton.Models
{
    // Join entity between Alumno and Curso with extra attributes
    public class Performance
    {
        public int AlumnoDni { get; set; }
        public Alumno Alumno { get; set; } = null!;

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;

        public int Inasistencias { get; set; }
        public int ProgresoGeneral { get; set; }
        public int Racha { get; set; }
    }
}
