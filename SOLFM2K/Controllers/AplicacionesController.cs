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
    [Route("api/[controller]")]
    [ApiController]
    //hola mundo 
    public class AplicacionesController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public AplicacionesController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Aplicaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aplicacione>>> GetAplicaciones()
        {
          if (_context.Aplicaciones == null)
          {
              return NotFound();
          }
            return await _context.Aplicaciones.ToListAsync();
        }

        // GET: api/Aplicaciones/5
        [HttpGet("{ApCodigo}")]
        public async Task<ActionResult<Aplicacione>> GetAplicacione(int ApCodigo)
        {
          if (_context.Aplicaciones == null)
          {
              return NotFound();
          }
            var aplicacione = await _context.Aplicaciones.FindAsync(ApCodigo);

            if (aplicacione == null)
            {
                return NotFound();
            }

            return aplicacione;
        }

        // PUT: api/Aplicaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ApCodigo}")]
        public async Task<IActionResult> PutAplicacione(int ApCodigo, Aplicacione aplicacione)
        {
            if (ApCodigo != aplicacione.ApCodigo)
            {
                return BadRequest();
            }

            _context.Entry(aplicacione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AplicacioneExists(ApCodigo))
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

        // POST: api/Aplicaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aplicacione>> PostAplicacione(Aplicacione aplicacione)
        {
          if (_context.Aplicaciones == null)
          {
              return Problem("Entity set 'SolicitudContext.Aplicaciones'  is null.");
          }
            _context.Aplicaciones.Add(aplicacione);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostAplicacione), new { id = aplicacione.ApCodigo }, aplicacione);
        }

        // DELETE: api/Aplicaciones/5
        [HttpDelete("{ApCodigo}")]
        public async Task<IActionResult> DeleteAplicacione(int ApCodigo)
        {
            if (_context.Aplicaciones == null)
            {
                return NotFound();
            }
            var aplicacione = await _context.Aplicaciones.FindAsync(ApCodigo);
            if (aplicacione == null)
            {
                return NotFound();
            }

            _context.Aplicaciones.Remove(aplicacione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AplicacioneExists(int ApCodigo)
        {
            return (_context.Aplicaciones?.Any(e => e.ApCodigo == ApCodigo)).GetValueOrDefault();
        }
    }
}
