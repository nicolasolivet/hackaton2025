using Microsoft.AspNetCore.Mvc;

namespace ctrlcctrlv.Controllers
{
    public class AlumnosController : Controller
    {
        private void PopulateAlumnoMenuCountsMock()
        {
            ViewBag.MensajesCount = 3;
            ViewBag.TareasCount = 2;
            ViewBag.AvisosCount = 1;
            ViewBag.InasistenciasCount = 0;

            ViewBag.AlumnoNombre = "Juan Pérez";
            ViewBag.AlumnoId = "A-1023";
            ViewBag.AvatarUrl = ""; 
        }
        public IActionResult Alumnos() 
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Materias()
        {
            ViewBag.Materias = new[]
            {
        new { Id = 1, Nombre = "Matemática", Profesor = "Lic. López", Horario = "Lun 10:00 - 12:00", Descripcion = "Álgebra y geometría" },
        new { Id = 2, Nombre = "Lengua", Profesor = "Mg. García", Horario = "Mar 8:00 - 10:00", Descripcion = "Lectura y redacción" },
        new { Id = 3, Nombre = "Historia", Profesor = "Prof. Díaz", Horario = "Mié 14:00 - 16:00", Descripcion = "Historia contemporánea" }
    };
            return View();
        }
        public IActionResult Avisos()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Inasistencias()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Cronograma()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Calificaciones()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult AlumnoMenu()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Perfil()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Tareas()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Mensajes()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Ficha()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }
        public IActionResult Soporte()
        {
            PopulateAlumnoMenuCountsMock();
            return View();
        }

        public IActionResult MateriaDetalle(int id)
        {
            var materias = new[]
            {
        new { Id = 1, Nombre = "Matemática", Profesor = "Lic. López", Horario = "Lun 10:00 - 12:00", Descripcion = "Álgebra y geometría" },
        new { Id = 2, Nombre = "Lengua", Profesor = "Mg. García", Horario = "Mar 8:00 - 10:00", Descripcion = "Lectura y redacción" },
        new { Id = 3, Nombre = "Historia", Profesor = "Prof. Díaz", Horario = "Mié 14:00 - 16:00", Descripcion = "Historia contemporánea" }
    };

            var materia = materias.FirstOrDefault(m => m.Id == id);
            if (materia == null) return NotFound();

            var tareas = new[]
            {
        new { Id = 1, Titulo = "Ejercicios p.34", FechaEntrega = new DateTime(2025,12,10), MateriaId = 1, Descripcion = "Resolver ejercicios 1-10" },
        new { Id = 2, Titulo = "Problemas de geometría", FechaEntrega = new DateTime(2025,12,15), MateriaId = 1, Descripcion = "Resolver en hoja aparte" },
        new { Id = 3, Titulo = "Leer capítulo 5", FechaEntrega = new DateTime(2025,12,09), MateriaId = 2, Descripcion = "Subir resumen" },
        new { Id = 4, Titulo = "Trabajo de investigación", FechaEntrega = new DateTime(2025,12,20), MateriaId = 3, Descripcion = "Grupo 3" }
    };

            var tareasMateria = tareas.Where(t => t.MateriaId == id).ToArray();

            ViewBag.Materia = materia;
            ViewBag.Tareas = tareasMateria;

            return View();
        }

        public IActionResult TareaDetalle(int id)
        {
            ViewBag.TareaId = id;
            return View();
        }
        public IActionResult Quiz(int materiaId)
        {
            // preguntas por materia (mock)
            var preguntasAll = new Dictionary<int, object[]>
    {
        { 1, new object[]
            {
                new { Id = 1, Pregunta = "¿2+2=?", Opciones = new[]{ "3","4","5" }, Respuesta = 1 },
                new { Id = 2, Pregunta = "¿5*3=?", Opciones = new[]{ "15","10","8" }, Respuesta = 0 }
            }
        },
        { 2, new object[]
            {
                new { Id = 1, Pregunta = "¿Cuál es una vocal?", Opciones = new[]{ "B","A","C" }, Respuesta = 1 }
            }
        },
        { 3, new object[]
            {
                new { Id = 1, Pregunta = "¿En qué año empezó la Revolución Francesa?", Opciones = new[]{ "1789","1800","1776" }, Respuesta = 0 }
            }
        }
    };

            preguntasAll.TryGetValue(materiaId, out var preguntas);
            ViewBag.MateriaId = materiaId;
            ViewBag.Preguntas = preguntas ?? new object[0];
            return View();
        }

        public IActionResult Juegos(int materiaId)
        {
            var juegosPorMateria = new Dictionary<int, object[]>
    {
        { 1, new object[] { new { Id="memoria", Titulo="Memoria Matemática" } } },
        { 2, new object[] { new { Id="ordenar", Titulo="Ordenar frases" } } },
        { 3, new object[] { new { Id="matching", Titulo="Emparejar fechas" } } }
    };

            juegosPorMateria.TryGetValue(materiaId, out var juegos);
            ViewBag.MateriaId = materiaId;
            ViewBag.Juegos = juegos ?? new object[0];
            return View();
        }

        public IActionResult Chatbot(int materiaId)
        {
            ViewBag.MateriaId = materiaId;
            ViewBag.MateriaNombre = "Materia " + materiaId;
            return View();
        }

    }
}
