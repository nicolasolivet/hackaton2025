using System.ComponentModel.DataAnnotations;
namespace APIhackaton.Models
{
    public class Tutor : Persona
    {
        public int ContadorAlertas { get; set; }

        public System.Collections.Generic.ICollection<TutorAlumno> TutorAlumnos { get; set; }

        public Tutor() { TutorAlumnos = new System.Collections.Generic.List<TutorAlumno>(); }

        public Tutor(int dni, string nombre, string apellido, string email, int rol, bool estado)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.rol = rol;
            this.estado = estado;
            this.TutorAlumnos = new System.Collections.Generic.List<TutorAlumno>();
        }
    }
}