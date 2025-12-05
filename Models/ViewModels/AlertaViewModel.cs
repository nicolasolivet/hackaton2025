namespace ctrlcctrlv.Models.ViewModels
{
    public class AlertaViewModel
    {
        public string Nivel { get; set; } = "info"; // "danger","warning","info","success"
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
