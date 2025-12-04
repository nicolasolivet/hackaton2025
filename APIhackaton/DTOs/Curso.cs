namespace APIhackaton.DTOs
{
    public class Curso
    {
        public int Anio { get; set; }
        public int comision { get; set; }
        public string Turno { get; set; }
        public string codigo { get; set; }
        public int dniDocente { get; set; }
        public ICollection <Alumno> Alumnos { get; set; }


    }
}
