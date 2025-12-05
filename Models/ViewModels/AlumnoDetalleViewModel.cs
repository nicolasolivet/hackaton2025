using System.Collections.Generic;

namespace ctrlcctrlv.Models.ViewModels
{
    public class AlumnoDetalleViewModel
    {
        // datos básicos (mapear desde tu Alumno model)
        public int Dni { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Curso { get; set; } = string.Empty;
        public int Edad { get; set; }

        // Informes
        public Dictionary<string, double> PromediosPorMateria { get; set; } = new();
        public List<string> Observaciones { get; set; } = new();
        public List<string> AlergiasOAlertas { get; set; } = new();
    }
}
