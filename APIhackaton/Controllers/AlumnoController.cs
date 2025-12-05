using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIhackaton.Data;
using APIhackaton.Models;

namespace APIhackaton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AlumnoController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Alumnos.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] APIhackaton.DTOs.PersonaCreateDto dto)
        {
            if (dto == null) return BadRequest();

            // dni must be provided and unique
            var exists = await _db.Alumnos.AnyAsync(a => a.dni == dto.dni) || await _db.Personas.AnyAsync(p => p.dni == dto.dni);
            if (exists) return Conflict(new { error = $"Persona with dni {dto.dni} already exists" });

            var alumno = new Alumno
            {
                dni = dto.dni,
                nombre = dto.nombre,
                apellido = dto.apellido,
                email = dto.email,
                rol = dto.rol,
                estado = dto.estado
            };

            _db.Alumnos.Add(alumno);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWithPerformances), new { dni = alumno.dni }, new { alumno.dni, alumno.nombre, alumno.apellido, alumno.email, alumno.rol, alumno.estado });
        }

        // GET api/alumno/{dni} -> returns alumno with performances and course->materia
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetWithPerformances(int dni)
        {
            // Project the alumno and its performances to avoid circular navigation properties
            var alumno = await _db.Alumnos
                .Where(a => a.dni == dni)
                .Select(a => new
                {
                    a.dni,
                    a.nombre,
                    a.apellido,
                    a.email,
                    a.rol,
                    a.estado,
                    Performances = a.Performances.Select(p => new
                    {
                        p.CursoId,
                        p.Inasistencias,
                        p.ProgresoGeneral,
                        p.Racha,
                        Curso = p.Curso == null ? null : new {
                            p.Curso.Id,
                            p.Curso.Anio,
                            p.Curso.comision,
                            p.Curso.Turno,
                            p.Curso.codigo,
                            Materia = p.Curso.Materia == null ? null : new { p.Curso.Materia.codigo, p.Curso.Materia.nombre }
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (alumno == null) return NotFound();
            return Ok(alumno);
        }
    }
}
