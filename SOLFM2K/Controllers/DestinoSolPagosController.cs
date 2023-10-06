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
    public class DestinoSolPagosController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public DestinoSolPagosController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/DestinoSolPagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestinoSolPago>>> GetDestinoSolPagos()
        {
          if (_context.DestinoSolPagos == null)
          {
              return NotFound();
          }
            return await _context.DestinoSolPagos.ToListAsync();
        }

        // GET: api/DestinoSolPagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestinoSolPago>> GetDestinoSolPago(int id)
        {
          if (_context.DestinoSolPagos == null)
          {
              return NotFound();
          }
            var destinoSolPago = await _context.DestinoSolPagos.FindAsync(id);

            if (destinoSolPago == null)
            {
                return NotFound();
            }

            return destinoSolPago;
        }

        // PUT: api/DestinoSolPagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestinoSolPago(int id, DestinoSolPago destinoSolPago)
        {
            if (id != destinoSolPago.DestPagId)
            {
                return BadRequest();
            }

            _context.Entry(destinoSolPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinoSolPagoExists(id))
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

        // POST: api/DestinoSolPagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DestinoSolPago>> PostDestinoSolPago(DestinoSolPago destinoSolPago)
        {
          if (_context.DestinoSolPagos == null)
          {
              return Problem("Entity set 'SolicitudContext.DestinoSolPagos'  is null.");
          }
            _context.DestinoSolPagos.Add(destinoSolPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestinoSolPago", new { id = destinoSolPago.DestPagId }, destinoSolPago);
        }

        // DELETE: api/DestinoSolPagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestinoSolPago(int id)
        {
            if (_context.DestinoSolPagos == null)
            {
                return NotFound();
            }
            var destinoSolPago = await _context.DestinoSolPagos.FindAsync(id);
            if (destinoSolPago == null)
            {
                return NotFound();
            }

            _context.DestinoSolPagos.Remove(destinoSolPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinoSolPagoExists(int id)
        {
            return (_context.DestinoSolPagos?.Any(e => e.DestPagId == id)).GetValueOrDefault();
        }
    }
}
