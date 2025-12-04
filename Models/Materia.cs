using ctrlcctrlv.Models;

namespace ctrlcctrlv.Models
{
    public class Materia
    {
        public string codigo { get; set; }

        public string nombre { get; set; }

        public Materia(string codigo, string nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
        }
    }
}