using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ctrlcctrlv.Models;
using ctrlcctrlv.Models.ViewModels;

namespace ctrlcctrlv.Controllers
{
    public class TutoresController : Controller
    {
        // CONTROLADOR SIN BD - MOCKS
        // Versión extendida para calificaciones trimestrales, gráficas, timeline y alertas.

        public IActionResult MenuTutor(int? tutorDni)
        {
            var vm = new TutorMenuViewModel();

            // Mock tutor/alumno
            vm.Tutor = new Tutor(tutorDni ?? 0, "María", "Pérez", "maria@mail.com", 2, true);
            vm.AlumnoACargo = new Alumno(12345678, "Juan", "Pérez", "juan@mail.com", 3, true);

            // Counters
            vm.TotalTareasPendientes = 3;
            vm.TotalMensajesNuevos = 1;
            vm.ProxEventos = 2;
            vm.UltimosComunicados = new List<string>
            {
                "Reunión de padres: 10/12/2025",
                "Entrega de boletines: 20/12/2025"
            };

            // --- Promedios por trimestre (mock) ---
            vm.PromediosTrimestrales = new List<PromedioTrimestral>
            {
                new PromedioTrimestral { Trimestre = "1° Trimestre", Promedio = 7.2 },
                new PromedioTrimestral { Trimestre = "2° Trimestre", Promedio = 7.8 },
                new PromedioTrimestral { Trimestre = "3° Trimestre", Promedio = 8.1 }
            };

            // --- Alertas (generadas según mocks) ---
            vm.Alertas = GenerateAlertasMock();

            return View(vm);
        }

        // Detalle del alumno (igual que antes)
        public IActionResult Alumno(int dni)
        {
            var vm = new AlumnoDetalleViewModel
            {
                Dni = dni,
                NombreCompleto = "Juan Pérez",
                Curso = "3°A",
                Edad = 14,
                PromediosPorMateria = new Dictionary<string, double>
                {
                    { "Matemática", 7.8 },
                    { "Lengua", 6.9 }
                },
                Observaciones = new List<string> { "Buen rendimiento en matemática" },
                AlergiasOAlertas = new List<string>()
            };

            return View(vm);
        }

        // Progreso: también devolvemos datos para Chart.js (series por materia)
        public IActionResult Progreso(int alumnoDni)
        {
            // Mock materias y evolución (3 trimestres)
            var materias = new List<MateriaProgreso>
            {
                new MateriaProgreso { NombreMateria = "Matemática", UltimaNota = 8, Promedio = 7.5, Observacion = "Mejora" },
                new MateriaProgreso { NombreMateria = "Lengua", UltimaNota = 7, Promedio = 6.8, Observacion = "Refuerzo lectura" }
            };

            // Datos para Chart.js (mock)
            // etiquetas (trimestres)
            ViewBag.ChartLabels = new[] { "1°", "2°", "3°" };

            // datasets: cada materia -> notas por trimestre
            ViewBag.ChartDatasets = new[]
            {
                new {
                    label = "Matemática",
                    data = new double[] { 6.5, 7.0, 8.0 }
                },
                new {
                    label = "Lengua",
                    data = new double[] { 6.0, 6.0, 7.0 }
                }
            };

            var vm = new ProgresoViewModel
            {
                AlumnoDni = alumnoDni,
                AlumnoNombre = "Juan Pérez",
                Materias = materias
            };

            return View(vm);
        }

        // Horarios + timeline
        public IActionResult Horarios(int alumnoDni)
        {
            ViewBag.Horario = new List<(string Dia, string Horario, string Descripcion)>
            {
                ("Lunes", "08:00 - 12:00", "Clases presenciales"),
                ("Martes", "10:30 - 11:30", "Reunión con tutor"),
                ("Miércoles", "08:00 - 12:00", "Clases presenciales")
            };

            ViewBag.Eventos = new List<(string Fecha, string Titulo, string Detalle)>
            {
                ("10/12/2025", "Reunión de padres", "18:00 - Aula Magna"),
                ("09/12/2025", "Actividad de Matemática", "Proyecto: resolución de problemas"),
                ("20/12/2025", "Entrega de boletines", "Todas las divisiones")
            };

            // timeline items: ordenados cronológicamente
            ViewBag.Timeline = new List<(string Fecha, string Titulo, string Descripcion)>
            {
                ("09/12/2025","Actividad de Matemática","Proyecto grupal"),
                ("10/12/2025","Reunión de padres","18:00 - Aula Magna"),
                ("20/12/2025","Entrega de boletines","Todas las divisiones")
            };

            return View();
        }

        public IActionResult Comunicados()
        {
            ViewBag.Comunicados = new List<(string Fecha, string Titulo)>
            {
                ("10/12/2025", "Reunión de padres"),
                ("01/12/2025", "Feriado local")
            };
            return View();
        }

        public IActionResult Mensajes()
        {
            ViewBag.Mensajes = new List<object>
            {
                new { Remitente = "Prof. García", Texto = "Hola, comento el avance en lectura.", Fecha = "01/12/2025" }
            };
            return View();
        }

        public IActionResult Asistencias(int alumnoDni)
        {
            ViewBag.Panels = new List<(string, List<(string, string, string)>)>
            {
                ("Matemática - 3°A", new List<(string,string,string)>
                {
                    ("2025-12-01", "Presente", "-"),
                    ("2025-11-20", "Ausente", "Enfermedad")
                }),
                ("Lengua - 3°A", new List<(string,string,string)>
                {
                    ("2025-12-01", "Presente", "-")
                })
            };
            return View();
        }

        public IActionResult Tareas(int alumnoDni)
        {
            ViewBag.Tareas = new List<object>
            {
                new { Materia = "Matemática", Titulo = "Ejercicios página 34", FechaEntrega = "2025-12-10", Estado = "Pendiente" }
            };
            return View();
        }

        public IActionResult Configuracion()
        {
            return View();
        }

        // --------------------------
        // Helpers (mocks)
        // --------------------------
        private List<AlertaViewModel> GenerateAlertasMock()
        {
            var lista = new List<AlertaViewModel>();

            // Ejemplo: nota baja
            lista.Add(new AlertaViewModel
            {
                Nivel = "danger",
                Titulo = "Promedio bajo en Matemática",
                Mensaje = "Promedio actual 5.8 — se recomienda refuerzo."
            });

            // Ausencias
            lista.Add(new AlertaViewModel
            {
                Nivel = "warning",
                Titulo = "Inasistencias",
                Mensaje = "2 inasistencias en el último mes."
            });

            // Si no hay alertas, mostraría success; aquí dejamos ambas
            lista.Add(new AlertaViewModel
            {
                Nivel = "info",
                Titulo = "Comportamiento",
                Mensaje = "Sin observaciones recientes."
            });

            return lista;
        }
    }
}
