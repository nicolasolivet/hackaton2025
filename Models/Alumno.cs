namespace ctrlcctrlv.Models
{
    public class Alumno
    {
        public int Dni { get; set;}
        
        //FK propiedad de navegacion base de datos
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public int IdTutor { get; set; }
        public Tutor Tutor { get; set;  }

    }
}
