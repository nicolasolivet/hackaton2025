using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIhackaton.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dni { get; set; }        // PK general para todas las personas (cliente-provided, no autoincrement)
        [Required]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public string apellido { get; set; } = string.Empty;

        [Required]
        public string email { get; set; } = string.Empty;
        [Required]

        public int rol { get; set; }

        [Required]
        public bool estado { get; set; }

        public Persona() { }
    }
}