using System.ComponentModel.DataAnnotations;
namespace APIhackaton.Models

{
    public class Materia
    {
        [Key]
        public string codigo { get; set; } = string.Empty;

        public string nombre { get; set; } = string.Empty;

        public Materia() { }

        public Materia(string codigo, string nombre)
        {
            this.codigo = codigo;
            this.nombre = nombre;
        }
    }
}