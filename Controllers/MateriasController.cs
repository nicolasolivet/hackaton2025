using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ctrlcctrlv.Controllers
{
    public class MateriaViewModel
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
    }

    public class MateriasController : Controller
    {
        // Datos de ejemplo en memoria (visual)
        private static readonly List<MateriaViewModel> _materias = new()
        {
            new MateriaViewModel { Codigo = "MAT01", Nombre = "Matemática" },
            new MateriaViewModel { Codigo = "LEN01", Nombre = "Lengua" },
            new MateriaViewModel { Codigo = "HIS01", Nombre = "Historia" },
            new MateriaViewModel { Codigo = "FIS01", Nombre = "Física" },
            new MateriaViewModel { Codigo = "BIO01", Nombre = "Biología" }
        };

        // GET: /Materias
        public IActionResult listaMaterias()
        {
            return View(_materias);
        }
    }
}