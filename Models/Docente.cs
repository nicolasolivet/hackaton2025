using ctrlcctrlv.Models;

namespace ctrlcctrlv.Models
{
    public class Docente : Persona
    {
        private int dniDocente { get; set; }
        public int MateriaId { get; set; }   // Relación hacia Materia (un docente da una materia)

        public Docente(int dniTutor, string nombre, string apellido, string email, int rol, bool estado)
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