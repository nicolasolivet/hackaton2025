namespace ctrlcctrlv.Models
{
    public class Persona
    {
        public int dni { get; set; }        // PK general para todas las personas
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public int rol { get; set; }

        public bool estado { get; set; }

        public Persona() { }
    }
}