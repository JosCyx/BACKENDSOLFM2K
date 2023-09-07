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
    public class DetSolPagoesController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public DetSolPagoesController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/DetSolPagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetSolPago>>> GetSolPagos()
        {
          if (_context.DetSolPagos == null)
          {
              return NotFound();
          }
            return await _context.DetSolPagos.ToListAsync();
        }

        // GET: api/DetSolPagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetSolPago>> GetDetSolPago(int id)
        {
          if (_context.DetSolPagos == null)
          {
              return NotFound();
          }
            var detSolPago = await _context.DetSolPagos.FindAsync(id);

            if (detSolPago == null)
            {
                return NotFound();
            }

            return detSolPago;
        }

        // PUT: api/DetSolPagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetSolPago(int id, DetSolPago detSolPago)
        {
            if (id != detSolPago.DetPagoID)
            {
                return BadRequest();
            }

            _context.Entry(detSolPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetSolPagoExists(id))
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

        // POST: api/DetSolPagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetSolPago>> PostDetSolPago(DetSolPago detSolPago)
        {
          if (_context.DetSolPagos == null)
          {
              return Problem("Entity set 'SolicitudContext.SolPagos'  is null.");
          }
            _context.DetSolPagos.Add(detSolPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDetSolPago), new { id = detSolPago.DetPagoID }, detSolPago);
        }

        // DELETE: api/DetSolPagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetSolPago(int id)
        {
            if (_context.DetSolPagos == null)
            {
                return NotFound();
            }
            var detSolPago = await _context.DetSolPagos.FindAsync(id);
            if (detSolPago == null)
            {
                return NotFound();
            }

            _context.DetSolPagos.Remove(detSolPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetSolPagoExists(int id)
        {
            return (_context.DetSolPagos?.Any(e => e.DetPagoID == id)).GetValueOrDefault();
        }
    }
}
