namespace APIhackaton.DTOs

{
    public class Docente 
    {
        public int dniDocente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int rol { get; set; }
        public bool estado { get; set; }   // Relación hacia Materia (un docente da una materia)

       
    }
}