using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIhackaton.Data;
using APIhackaton.Models;

namespace APIhackaton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TutorController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Tutores.Include(t => t.TutorAlumnos).ToListAsync());

        [HttpGet("{dni}")]
        public async Task<IActionResult> Get(int dni)
        {
            var item = await _db.Tutores.Include(t => t.TutorAlumnos).ThenInclude(ta => ta.Alumno).FirstOrDefaultAsync(t => t.dni == dni);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tutor tutor)
        {
            _db.Tutores.Add(tutor);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { dni = tutor.dni }, tutor);
        }

        [HttpPut("{dni}")]
        public async Task<IActionResult> Update(int dni, Tutor tutor)
        {
            if (dni != tutor.dni) return BadRequest();
            _db.Entry(tutor).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{dni}")]
        public async Task<IActionResult> Delete(int dni)
        {
            var tutor = await _db.Tutores.FindAsync(dni);
            if (tutor == null) return NotFound();
            _db.Tutores.Remove(tutor);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
