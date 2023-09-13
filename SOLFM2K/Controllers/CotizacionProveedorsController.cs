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
    public class CotizacionProveedorsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public CotizacionProveedorsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/CotizacionProveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CotizacionProveedor>>> GetCotizacionProveedors()
        {
          if (_context.CotizacionProveedors == null)
          {
              return NotFound();
          }
            return await _context.CotizacionProveedors.ToListAsync();
        }

        // GET: api/CotizacionProveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CotizacionProveedor>> GetCotizacionProveedor(int id)
        {
          if (_context.CotizacionProveedors == null)
          {
              return NotFound();
          }
            var cotizacionProveedor = await _context.CotizacionProveedors.FindAsync(id);

            if (cotizacionProveedor == null)
            {
                return NotFound();
            }

            return cotizacionProveedor;
        }

        // PUT: api/CotizacionProveedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotizacionProveedor(int id, CotizacionProveedor cotizacionProveedor)
        {
            if (id != cotizacionProveedor.CotProvId)
            {
                return BadRequest();
            }

            _context.Entry(cotizacionProveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotizacionProveedorExists(id))
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

        // POST: api/CotizacionProveedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CotizacionProveedor>> PostCotizacionProveedor(CotizacionProveedor cotizacionProveedor)
        {
          if (_context.CotizacionProveedors == null)
          {
              return Problem("Entity set 'SolicitudContext.CotizacionProveedors'  is null.");
          }
            _context.CotizacionProveedors.Add(cotizacionProveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCotizacionProveedor), new { id = cotizacionProveedor.CotProvId }, cotizacionProveedor);
        }

        // DELETE: api/CotizacionProveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotizacionProveedor(int id)
        {
            if (_context.CotizacionProveedors == null)
            {
                return NotFound();
            }
            var cotizacionProveedor = await _context.CotizacionProveedors.FindAsync(id);
            if (cotizacionProveedor == null)
            {
                return NotFound();
            }

            _context.CotizacionProveedors.Remove(cotizacionProveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CotizacionProveedorExists(int id)
        {
            return (_context.CotizacionProveedors?.Any(e => e.CotProvId == id)).GetValueOrDefault();
        }
    }
}
