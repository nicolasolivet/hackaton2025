
using System;

namespace APIhackaton.DTOs
{
    public class NotaDto
    {
        public int dniAlumno { get; set; }
        public string idMateria { get; set; } = string.Empty; // codigo materia
        public double valor { get; set; }
        public DateTime? fecha { get; set; } // si es null el servidor pondrá UTC now
    }
}
