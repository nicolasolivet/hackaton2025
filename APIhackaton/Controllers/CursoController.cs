using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIhackaton.Data;
using APIhackaton.Models;

namespace APIhackaton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CursoController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Return a projection to avoid circular references during JSON serialization
            var list = await _db.Cursos
                .Include(c => c.Docente)
                .Include(c => c.Materia)
                .Select(c => new
                {
                    c.Id,
                    c.Anio,
                    c.comision,
                    c.Turno,
                    c.codigo,
                    Docente = c.Docente == null ? null : new { c.Docente.dni, c.Docente.nombre, c.Docente.apellido, c.Docente.email },
                    Materia = c.Materia == null ? null : new { c.Materia.codigo, c.Materia.nombre }
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Cursos.Include(c => c.Performances).ThenInclude(p => p.Alumno).FirstOrDefaultAsync(c => c.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] APIhackaton.DTOs.CursoCreateDto dto)
        {
            // determine docente dni and codigo (allow either top-level or nested shape)
            var dniDocente = dto.dniDocente ?? dto.docente?.dni;
            var codigo = string.IsNullOrWhiteSpace(dto.codigo) ? dto.materia?.codigo : dto.codigo;

            if (dniDocente == null) return BadRequest(new { error = "dniDocente is required (or provide docente.dni)" });
            if (string.IsNullOrWhiteSpace(codigo)) return BadRequest(new { error = "codigo (materia) is required" });

            // validate referenced Docente and Materia exist
            var docente = await _db.Docentes.FindAsync(dniDocente.Value);
            if (docente == null) return BadRequest(new { error = $"Docente with dni {dniDocente} not found" });

            var materia = await _db.Materias.FindAsync(codigo);
            if (materia == null) return BadRequest(new { error = $"Materia with codigo '{codigo}' not found" });

            var curso = new Curso
            {
                Anio = dto.Anio,
                comision = dto.comision,
                Turno = dto.Turno,
                codigo = codigo,
                dniDocente = dniDocente.Value
            };

            _db.Cursos.Add(curso);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = curso.Id }, new {
                curso.Id, curso.Anio, curso.comision, curso.Turno, curso.codigo,
                Docente = new { docente!.dni, docente.nombre, docente.apellido, docente.email },
                Materia = new { materia!.codigo, materia.nombre }
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Curso curso)
        {
            if (id != curso.Id) return BadRequest();
            _db.Entry(curso).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _db.Cursos.FindAsync(id);
            if (curso == null) return NotFound();
            _db.Cursos.Remove(curso);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
