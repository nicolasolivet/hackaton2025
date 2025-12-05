using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIhackaton.Data;
using APIhackaton.Models;

namespace APIhackaton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PerformanceController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Return a projection to avoid circular references (Alumno -> Performances -> Alumno ...)
            var list = await _db.Performances
                .Include(p => p.Alumno)
                .Include(p => p.Curso)
                    .ThenInclude(c => c.Materia)
                .Select(p => new
                {
                    p.AlumnoDni,
                    p.CursoId,
                    p.Inasistencias,
                    p.ProgresoGeneral,
                    p.Racha,
                    Alumno = p.Alumno == null ? null : new { p.Alumno.dni, p.Alumno.nombre, p.Alumno.apellido, p.Alumno.email },
                    Curso = p.Curso == null ? null : new { p.Curso.Id, p.Curso.Anio, p.Curso.comision, p.Curso.Turno, p.Curso.codigo,
                        Materia = p.Curso.Materia == null ? null : new { p.Curso.Materia.codigo, p.Curso.Materia.nombre }
                    }
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{alumnoDni}/{cursoId}")]
        public async Task<IActionResult> Get(int alumnoDni, int cursoId)
        {
            var item = await _db.Performances
                .Where(p => p.AlumnoDni == alumnoDni && p.CursoId == cursoId)
                .Include(p => p.Alumno)
                .Include(p => p.Curso)
                    .ThenInclude(c => c.Materia)
                .Select(p => new
                {
                    p.AlumnoDni,
                    p.CursoId,
                    p.Inasistencias,
                    p.ProgresoGeneral,
                    p.Racha,
                    Alumno = p.Alumno == null ? null : new { p.Alumno.dni, p.Alumno.nombre, p.Alumno.apellido, p.Alumno.email },
                    Curso = p.Curso == null ? null : new { p.Curso.Id, p.Curso.Anio, p.Curso.comision, p.Curso.Turno, p.Curso.codigo,
                        Materia = p.Curso.Materia == null ? null : new { p.Curso.Materia.codigo, p.Curso.Materia.nombre }
                    }
                })
                .FirstOrDefaultAsync();

            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] APIhackaton.DTOs.PerformanceCreateDto dto)
        {
            // Validate referenced Alumno and Curso exist
            var alumno = await _db.Alumnos.FindAsync(dto.AlumnoDni);
            if (alumno == null) return BadRequest(new { error = $"Alumno with dni {dto.AlumnoDni} not found" });

            var curso = await _db.Cursos.FindAsync(dto.CursoId);
            if (curso == null) return BadRequest(new { error = $"Curso with id {dto.CursoId} not found" });

            var perf = new Performance
            {
                AlumnoDni = dto.AlumnoDni,
                CursoId = dto.CursoId,
                Inasistencias = dto.Inasistencias,
                ProgresoGeneral = dto.ProgresoGeneral,
                Racha = dto.Racha
            };

            _db.Performances.Add(perf);
            await _db.SaveChangesAsync();

            // return a compact projection
            return CreatedAtAction(nameof(Get), new { alumnoDni = perf.AlumnoDni, cursoId = perf.CursoId }, new
            {
                perf.AlumnoDni,
                perf.CursoId,
                perf.Inasistencias,
                perf.ProgresoGeneral,
                perf.Racha
            });
        }

        [HttpPut("{alumnoDni}/{cursoId}")]
        public async Task<IActionResult> Update(int alumnoDni, int cursoId, Performance perf)
        {
            if (alumnoDni != perf.AlumnoDni || cursoId != perf.CursoId) return BadRequest();
            _db.Entry(perf).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{alumnoDni}/{cursoId}")]
        public async Task<IActionResult> Delete(int alumnoDni, int cursoId)
        {
            var item = await _db.Performances.FindAsync(alumnoDni, cursoId);
            if (item == null) return NotFound();
            _db.Performances.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
