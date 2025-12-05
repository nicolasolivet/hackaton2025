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
        public async Task<IActionResult> GetAll() => Ok(await _db.Performances.Include(p => p.Alumno).Include(p => p.Curso).ToListAsync());

        [HttpGet("{alumnoDni}/{cursoId}")]
        public async Task<IActionResult> Get(int alumnoDni, int cursoId)
        {
            var item = await _db.Performances.Include(p => p.Alumno).Include(p => p.Curso)
                .FirstOrDefaultAsync(p => p.AlumnoDni == alumnoDni && p.CursoId == cursoId);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Performance perf)
        {
            _db.Performances.Add(perf);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { alumnoDni = perf.AlumnoDni, cursoId = perf.CursoId }, perf);
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
