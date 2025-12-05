// ctrlcctrlv/Controllers/NotasController.cs
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

using APIhackaton.Data;
using APIhackaton.Models;
using APIhackaton.DTOs;

namespace ctrlcctrlv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        // POST: api/Notas
        [HttpPost]
        public ActionResult<Nota> Create([FromBody] NotaDto dto)
        {
            if (dto == null) return BadRequest("Body vacío");

            // validar alumno
            if (!FakeDataContext.Alumnos.Any(a => a.dni == dto.dniAlumno))
                return BadRequest("El alumno no existe.");

            // validar materia
            if (!FakeDataContext.Materias.Any(m => m.codigo == dto.idMateria))
                return BadRequest("La materia no existe.");

            if (dto.valor < 1 || dto.valor > 10) return BadRequest("La nota debe estar entre 1 y 10.");

            var nota = new Nota
            {
                dniAlumno = dto.dniAlumno,
                idMateria = dto.idMateria,
                valor = dto.valor,
                fecha = dto.fecha ?? DateTime.UtcNow
            };

            var created = FakeDataContext.AddNota(nota);

            return CreatedAtAction(nameof(GetByAlumno), new { dniAlumno = created.dniAlumno }, created);
        }

        // GET: api/Notas/alumno/{dniAlumno}
        [HttpGet("alumno/{dniAlumno:int}")]
        public ActionResult<IEnumerable<Nota>> GetByAlumno(int dniAlumno)
        {
            var notas = FakeDataContext.Notas
                .Where(n => n.dniAlumno == dniAlumno)
                .OrderByDescending(n => n.fecha)
                .ToList();

            return Ok(notas);
        }
    }
}
