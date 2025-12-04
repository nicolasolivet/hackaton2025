namespace APIhackaton.DTOs
{
    public class Persona
    {
        private int dni { get; set; }        // PK general para todas las personas
        private string nombre { get; set; } = string.Empty;
        private string apellido { get; set; } = string.Empty;

        private string email { get; set; } = string.Empty;

        private int rol { get; set; }

        private bool estado { get; set; }

    }
}