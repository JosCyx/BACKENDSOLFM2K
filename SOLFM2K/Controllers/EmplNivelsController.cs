using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

namespace SOLFM2K.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class EmplNivelsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public EmplNivelsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/EmplNivels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmplNivel>>> GetEmpleadoNivel()
        {
          if (_context.EmpleadoNivel == null)
          {
              return NotFound();
          }
            return await _context.EmpleadoNivel.ToListAsync();
        }

        // GET: api/EmplNivels/5
        [HttpGet("GetEmplByDep")]
        public async Task<ActionResult<IEnumerable<EmplNivel>>> GetEmplByDep(int dep, int nivel)
        {
            var emplNivel = await _context.EmpleadoNivel.Where(emp => emp.EmpNivDeptAutorizado == dep && emp.EmpNivRuteo == nivel).ToListAsync();

            if (emplNivel == null)
            {
                return NotFound();
            }

            return emplNivel;
        }

        // PUT: api/EmplNivels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmplNivel(int id, EmplNivel emplNivel)
        {
            if (id != emplNivel.EmpNivId)
            {
                return BadRequest();
            }

            _context.Entry(emplNivel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmplNivelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmplNivels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmplNivel>> PostEmplNivel(EmplNivel emplNivel)
        {
          /*if (_context.EmpleadoNivel == null)
          {
              return Problem("Entity set 'SolicitudContext.EmpleadoNivel'  is null.");
          }*/
            _context.EmpleadoNivel.Add(emplNivel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostEmplNivel), new { id = emplNivel.EmpNivId }, emplNivel);
        }

        // DELETE: api/EmplNivels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmplNivel(int id)
        {
            if (_context.EmpleadoNivel == null)
            {
                return NotFound();
            }
            var emplNivel = await _context.EmpleadoNivel.FindAsync(id);
            if (emplNivel == null)
            {
                return NotFound();
            }

            _context.EmpleadoNivel.Remove(emplNivel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmplNivelExists(int id)
        {
            return (_context.EmpleadoNivel?.Any(e => e.EmpNivId == id)).GetValueOrDefault();
        }
    }
}
