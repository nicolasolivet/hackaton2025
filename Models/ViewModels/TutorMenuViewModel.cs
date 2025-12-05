using System.Collections.Generic;
using ctrlcctrlv.Models;

namespace ctrlcctrlv.Models.ViewModels
{
    public class TutorMenuViewModel
    {
        public Tutor Tutor { get; set; } = null!;
        public Alumno? AlumnoACargo { get; set; }

        public int TotalTareasPendientes { get; set; }
        public int TotalMensajesNuevos { get; set; }
        public int ProxEventos { get; set; }

        public List<string> UltimosComunicados { get; set; } = new();

        // Nuevos:
        public List<PromedioTrimestral> PromediosTrimestrales { get; set; } = new();
        public List<AlertaViewModel> Alertas { get; set; } = new();
    }
}
