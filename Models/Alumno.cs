namespace ctrlcctrlv.Models
{
    public class Alumno : Persona
    {
        private int dniAlumno;

        public Alumno(int dniAlumno, string nombre, string apellido, string email, int rol, bool estado)
        {
            this.dniAlumno = dniAlumno;
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.rol = rol;
            this.estado = estado;
        }
    }
}

