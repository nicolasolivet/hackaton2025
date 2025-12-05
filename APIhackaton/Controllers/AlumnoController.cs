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

        // GET api/alumno/{dni} -> returns alumno with performances and course->materia
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetWithPerformances(int dni)
        {
            var alumno = await _db.Alumnos
                .Include(a => a.Performances)
                    .ThenInclude(p => p.Curso)
                        .ThenInclude(c => c.Materia)
                .FirstOrDefaultAsync(a => a.dni == dni);

            if (alumno == null) return NotFound();
            return Ok(alumno);
        }
    }
}
