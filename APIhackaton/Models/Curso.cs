using System.ComponentModel.DataAnnotations;

namespace APIhackaton.Models
{
    public class Curso
    {
        // Primary key
        public int Id { get; set; }

        public int Anio { get; set; }
        public int comision { get; set; }
        public string Turno { get; set; } = string.Empty;
        public string codigo { get; set; } = string.Empty;

        public int dniDocente { get; set; }
        public Curso() {
            Performances = new System.Collections.Generic.List<Performance>();
        }

        public Curso(int anio, int comision, string turno, string codigo, int dniDocente)
        {
            this.Anio = anio;
            this.comision = comision;
            this.Turno = turno;
            this.codigo = codigo;
            this.dniDocente = dniDocente;
            this.Performances = new System.Collections.Generic.List<Performance>();
        }

        public System.Collections.Generic.ICollection<Performance> Performances { get; set; } = new System.Collections.Generic.List<Performance>();
        public Docente? Docente { get; set; }
        public Materia? Materia { get; set; }
    }
}
