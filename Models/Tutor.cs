namespace ctrlcctrlv.Models
{
    public class Tutor : Persona
    {
        private int dniTutor { get; set; }
        public Tutor(int dniTutor, string nombre, string apellido, string email, int rol, bool estado)
        {
            this.dni = dniTutor;
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.rol = rol;
            this.estado = estado;
        }
    }
}