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
        public async Task<IActionResult> GetAll() => Ok(await _db.Cursos.Include(c => c.Docente).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Cursos.Include(c => c.Performances).ThenInclude(p => p.Alumno).FirstOrDefaultAsync(c => c.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Curso curso)
        {
            _db.Cursos.Add(curso);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = curso.Id }, curso);
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
