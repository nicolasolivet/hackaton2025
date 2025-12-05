using System.Collections.Generic;

namespace ctrlcctrlv.Models.ViewModels
{
    public class ProgresoViewModel
    {
        public int AlumnoDni { get; set; }
        public string AlumnoNombre { get; set; } = string.Empty;

        public List<MateriaProgreso> Materias { get; set; } = new();
    }

    public class MateriaProgreso
    {
        public string NombreMateria { get; set; } = string.Empty;
        public double UltimaNota { get; set; }
        public double Promedio { get; set; }
        public string Observacion { get; set; } = string.Empty;
    }
}
