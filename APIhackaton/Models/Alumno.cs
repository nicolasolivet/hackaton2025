using System.ComponentModel.DataAnnotations;

namespace APIhackaton.Models
{
    public class Alumno : Persona
    {
        public Alumno() { }

        public Alumno(int dni, string nombre, string apellido, string email, int rol, bool estado)
        {
            this.dni = dni;
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.rol = rol;
            this.estado = estado;
        }
        public System.Collections.Generic.ICollection<Performance> Performances { get; set; } = new System.Collections.Generic.List<Performance>();
        public System.Collections.Generic.ICollection<TutorAlumno> TutorAlumnos { get; set; } = new System.Collections.Generic.List<TutorAlumno>();
    }
}

