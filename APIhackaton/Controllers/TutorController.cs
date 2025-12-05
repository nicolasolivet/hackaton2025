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
        public async Task<IActionResult> GetAll()
        {
            // Return tutors (person data) by default. Do not include the join collection unless explicitly requested.
            var tutors = await _db.Tutores.ToListAsync();
            return Ok(tutors);
        }

        [HttpGet("{dni}")]
        public async Task<IActionResult> Get(int dni, [FromQuery] bool includeStudents = false)
        {
            if (includeStudents)
            {
                // Project tutor with minimal student info to avoid circular navigation serialization
                var proj = await _db.Tutores
                    .Where(t => t.dni == dni)
                    .Select(t => new
                    {
                        t.dni,
                        t.nombre,
                        t.apellido,
                        t.email,
                        t.rol,
                        t.estado,
                        Students = t.TutorAlumnos
                            .Select(ta => new {
                                ta.TutorDni,
                                ta.AlumnoDni,
                                Alumno = ta.Alumno == null ? null : new { ta.Alumno.dni, ta.Alumno.nombre, ta.Alumno.apellido, ta.Alumno.email }
                            })
                            .ToList()
                    })
                    .FirstOrDefaultAsync();

                if (proj == null) return NotFound();
                return Ok(proj);
            }

            var item = await _db.Tutores.FirstOrDefaultAsync(t => t.dni == dni);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] APIhackaton.DTOs.PersonaCreateDto dto)
        {
            // dni must be provided (Persona.dni is client-provided PK)
            if (dto == null) return BadRequest();

            // prevent duplicate dni
            var exists = await _db.Tutores.AnyAsync(t => t.dni == dto.dni) || await _db.Personas.AnyAsync(p => p.dni == dto.dni);
            if (exists) return Conflict(new { error = $"Persona with dni {dto.dni} already exists" });

            var tutor = new Tutor
            {
                dni = dto.dni,
                nombre = dto.nombre,
                apellido = dto.apellido,
                email = dto.email,
                rol = dto.rol,
                estado = dto.estado,
                ContadorAlertas = 0
            };

            _db.Tutores.Add(tutor);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { dni = tutor.dni }, new { tutor.dni, tutor.nombre, tutor.apellido, tutor.email, tutor.rol, tutor.estado, tutor.ContadorAlertas });
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
