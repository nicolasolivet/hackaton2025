using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIhackaton.Models

{
    public class Docente : Persona
    {
        // Un docente dicta muchos cursos; relación inversa:
        public System.Collections.Generic.ICollection<Curso> Cursos { get; set; } = new System.Collections.Generic.List<Curso>();

        public Docente() { }

        public Docente(int dniDocente, string nombre, string apellido, string email, int rol, bool estado)
        {
            this.dni = dniDocente;
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.rol = rol;
            this.estado = estado;
        }
    }
}