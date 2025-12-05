using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIhackaton.Data;
using APIhackaton.Models;

namespace APIhackaton.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PersonaController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Personas.ToListAsync());

        [HttpGet("{dni}")]
        public async Task<IActionResult> Get(int dni)
        {
            var p = await _db.Personas.FindAsync(dni);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
            _db.Personas.Add(persona);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { dni = persona.dni }, persona);
        }

        [HttpPut("{dni}")]
        public async Task<IActionResult> Update(int dni, Persona persona)
        {
            if (dni != persona.dni) return BadRequest();
            _db.Entry(persona).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{dni}")]
        public async Task<IActionResult> Delete(int dni)
        {
            var p = await _db.Personas.FindAsync(dni);
            if (p == null) return NotFound();
            _db.Personas.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
