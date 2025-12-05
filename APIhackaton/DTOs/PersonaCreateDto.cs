namespace APIhackaton.DTOs
{
    public class PersonaCreateDto
    {
        public int dni { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public int rol { get; set; }
        public bool estado { get; set; }
    }
}
