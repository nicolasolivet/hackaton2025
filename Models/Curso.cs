namespace ctrlcctrlv.Models
{
    public class Curso
    {
        public int CursoId { get; set; }
        public int Anio { get; set; }
        public int Division { get; set; }
        public string Turno { get; set; }

        public ICollection<Alumno> Alumnos { get; set; }


    }
}
