// ctrlcctrlv/Controllers/AlumnosController.cs
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

using ModelAlumno = APIhackaton.Models.Alumno;
using DTOAlumno = APIhackaton.DTOs.Alumno;
using APIhackaton.Data;

namespace ctrlcctrlv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnosController : ControllerBase
    {
        // GET: api/Alumnos
        [HttpGet]
        public ActionResult<IEnumerable<DTOAlumno>> GetAll()
        {
            var dtos = FakeDataContext.Alumnos.Select(a => new DTOAlumno
            {
                dniAlumno = a.dni,
                nombre = a.nombre,
                apellido = a.apellido,
                email = a.email,
                rol = a.rol,
                estado = a.estado
            }).ToList();

            return Ok(dtos);
        }

        // GET: api/Alumnos/{dni}
        [HttpGet("{dni:int}")]
        public ActionResult<DTOAlumno> Get(int dni)
        {
            var alumno = FakeDataContext.Alumnos.FirstOrDefault(a => a.dni == dni);
            if (alumno == null) return NotFound();

            var dto = new DTOAlumno
            {
                dniAlumno = alumno.dni,
                nombre = alumno.nombre,
                apellido = alumno.apellido,
                email = alumno.email,
                rol = alumno.rol,
                estado = alumno.estado
            };

            return Ok(dto);
        }

        // POST: api/Alumnos
        [HttpPost]
        public ActionResult Create([FromBody] DTOAlumno dto)
        {
            if (dto == null) return BadRequest("Body vacío");
            if (FakeDataContext.Alumnos.Any(a => a.dni == dto.dniAlumno))
                return BadRequest("Ya existe un alumno con ese DNI.");

            // crear usando tu constructor
            var nuevo = new ModelAlumno(dto.dniAlumno, dto.nombre, dto.apellido, dto.email, dto.rol, dto.estado)
            {
                dni = dto.dniAlumno
            };

            FakeDataContext.Alumnos.Add(nuevo);

            return CreatedAtAction(nameof(Get), new { dni = nuevo.dni }, dto);
        }

        // PUT: api/Alumnos/{dni}
        [HttpPut("{dni:int}")]
        public ActionResult Update(int dni, [FromBody] DTOAlumno dto)
        {
            var alumno = FakeDataContext.Alumnos.FirstOrDefault(a => a.dni == dni);
            if (alumno == null) return NotFound();

            alumno.nombre = dto.nombre;
            alumno.apellido = dto.apellido;
            alumno.email = dto.email;
            alumno.rol = dto.rol;
            alumno.estado = dto.estado;

            return NoContent();
        }

        // DELETE: api/Alumnos/{dni}  (marca como inactivo)
        [HttpDelete("{dni:int}")]
        public ActionResult Delete(int dni)
        {
            var alumno = FakeDataContext.Alumnos.FirstOrDefault(a => a.dni == dni);
            if (alumno == null) return NotFound();

            alumno.estado = false;
            return NoContent();
        }

        // GET: api/Alumnos/{dni}/riesgo
        [HttpGet("{dni:int}/riesgo")]
        public ActionResult<object> GetRiesgo(int dni)
        {
            var alumno = FakeDataContext.Alumnos.FirstOrDefault(a => a.dni == dni);
            if (alumno == null) return NotFound();

            var notasPorMateria = FakeDataContext.Notas
                .Where(n => n.dniAlumno == dni)
                .GroupBy(n => n.idMateria);

            bool enRiesgo = false;
            string? materiaAfectada = null;

            foreach (var grupo in notasPorMateria)
            {
                var ultimasDos = grupo.OrderByDescending(n => n.fecha).Take(2).ToList();
                if (ultimasDos.Count == 2 && ultimasDos.All(n => n.valor < 6))
                {
                    var mat = FakeDataContext.GetMateriaByCodigo(grupo.Key);
                    enRiesgo = true;
                    materiaAfectada = mat?.nombre ?? grupo.Key;
                    break;
                }
            }

            return Ok(new
            {
                dniAlumno = dni,
                alumno.nombre,
                alumno.apellido,
                Riesgo = enRiesgo,
                Materia = materiaAfectada
            });
        }
    }
}
