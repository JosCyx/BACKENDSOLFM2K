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
    public class TransaccionesController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public TransaccionesController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Transacciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccione>>> GetTransacciones()
        {
          if (_context.Transacciones == null)
          {
              return NotFound();
          }
            return await _context.Transacciones.ToListAsync();
        }

        // GET: api/Transacciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaccione>> GetTransaccione(int id)
        {
          if (_context.Transacciones == null)
          {
              return NotFound();
          }
            var transaccione = await _context.Transacciones.FindAsync(id);

            if (transaccione == null)
            {
                return NotFound();
            }

            return transaccione;
        }

        // PUT: api/Transacciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaccione(int id, Transaccione transaccione)
        {
            if (id != transaccione.TrCodigo)
            {
                return BadRequest();
            }

            _context.Entry(transaccione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccioneExists(id))
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

        // POST: api/Transacciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaccione>> PostTransaccione(Transaccione transaccione)
        {
          if (_context.Transacciones == null)
          {
              return Problem("Entity set 'SolicitudContext.Transacciones'  is null.");
          }
            _context.Transacciones.Add(transaccione);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTransaccione), new { id = transaccione.TrCodigo }, transaccione);
        }

        // DELETE: api/Transacciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccione(int id)
        {
            if (_context.Transacciones == null)
            {
                return NotFound();
            }
            var transaccione = await _context.Transacciones.FindAsync(id);
            if (transaccione == null)
            {
                return NotFound();
            }

            _context.Transacciones.Remove(transaccione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaccioneExists(int id)
        {
            return (_context.Transacciones?.Any(e => e.TrCodigo == id)).GetValueOrDefault();
        }
    }
}
