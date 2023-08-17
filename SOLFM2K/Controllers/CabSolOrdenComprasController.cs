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
    public class CabSolOrdenComprasController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public CabSolOrdenComprasController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/CabSolOrdenCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CabSolOrdenCompra>>> GetCabSolOrdenCompras()
        {
          if (_context.CabSolOrdenCompras == null)
          {
              return NotFound();
          }
            return await _context.CabSolOrdenCompras.ToListAsync();
        }

        // GET: api/CabSolOrdenCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CabSolOrdenCompra>> GetCabSolOrdenCompra(int id)
        {
          if (_context.CabSolOrdenCompras == null)
          {
              return NotFound();
          }
            var cabSolOrdenCompra = await _context.CabSolOrdenCompras.FindAsync(id);

            if (cabSolOrdenCompra == null)
            {
                return NotFound();
            }

            return cabSolOrdenCompra;
        }

        // PUT: api/CabSolOrdenCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabSolOrdenCompra(int id, CabSolOrdenCompra cabSolOrdenCompra)
        {
            if (id != cabSolOrdenCompra.cabSolOCID)
            {
                return BadRequest();
            }

            _context.Entry(cabSolOrdenCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabSolOrdenCompraExists(id))
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

        // POST: api/CabSolOrdenCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CabSolOrdenCompra>> PostCabSolOrdenCompra(CabSolOrdenCompra cabSolOrdenCompra)
        {
          if (_context.CabSolOrdenCompras == null)
          {
              return Problem("Entity set 'SolicitudContext.CabSolOrdenCompras'  is null.");
          }
            _context.CabSolOrdenCompras.Add(cabSolOrdenCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCabSolOrdenCompra", new { id = cabSolOrdenCompra.cabSolOCID }, cabSolOrdenCompra);
        }

        // DELETE: api/CabSolOrdenCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabSolOrdenCompra(int id)
        {
            if (_context.CabSolOrdenCompras == null)
            {
                return NotFound();
            }
            var cabSolOrdenCompra = await _context.CabSolOrdenCompras.FindAsync(id);
            if (cabSolOrdenCompra == null)
            {
                return NotFound();
            }

            _context.CabSolOrdenCompras.Remove(cabSolOrdenCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CabSolOrdenCompraExists(int id)
        {
            return (_context.CabSolOrdenCompras?.Any(e => e.cabSolOCID == id)).GetValueOrDefault();
        }
    }
}
