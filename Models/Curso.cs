namespace ctrlcctrlv.Models
{
    public class Curso
    {
        public int Anio { get; set; }
        public int comision { get; set; }
        public string Turno { get; set; }

        public string codigo { get; set; }

        public int dniDocente { get; set; }

        public Curso(int anio, int comision, string turno, string codigo, int dniDocente)
        {
            this.Anio = anio;
            this.comision = comision;
            this.Turno = turno;
            this.codigo = codigo;
            this.dniDocente = dniDocente;

        }

        public ICollection<Alumno> Alumnos
        { get; set; }


    }
}
