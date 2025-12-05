// APIhackaton/Models/Nota.cs
using System;

namespace APIhackaton.Models
{
    public class Nota
    {
        public int idNota { get; set; }
        public int dniAlumno { get; set; }   // referencia a Persona.dni
        public string idMateria { get; set; } = string.Empty; // codigo de Materia
        public double valor { get; set; }
        public DateTime fecha { get; set; }
    }
}
