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
    public class RuteosController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public RuteosController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Ruteos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruteo>>> GetRuteos()
        {
          if (_context.Ruteos == null)
          {
              return NotFound();
          }
            return await _context.Ruteos.ToListAsync();
        }

        // GET: api/Ruteos/5
        [HttpGet("{RutCodigo}")]
        public async Task<ActionResult<Ruteo>> GetRuteo(int RutCodigo)
        {
          if (_context.Ruteos == null)
          {
              return NotFound();
          }
            var ruteo = await _context.Ruteos.FindAsync(RutCodigo);

            if (ruteo == null)
            {
                return NotFound();
            }

            return ruteo;
        }

        // PUT: api/Ruteos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RutCodigo}")]
        public async Task<IActionResult> PutRuteo(int RutCodigo, Ruteo ruteo)
        {
            if (RutCodigo != ruteo.RutCod)
            {
                return BadRequest();
            }

            _context.Entry(ruteo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuteoExists(RutCodigo))
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

        // POST: api/Ruteos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ruteo>> PostRuteo(Ruteo ruteo)
        {
          if (_context.Ruteos == null)
          {
              return Problem("Entity set 'SolicitudContext.Ruteos'  is null.");
          }
            _context.Ruteos.Add(ruteo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuteo", new { RutCodigo = ruteo.RutCod }, ruteo);
        }

        // DELETE: api/Ruteos/5
        [HttpDelete("{RutCodigo}")]
        public async Task<IActionResult> DeleteRuteo(int RutCodigo)
        {
            if (_context.Ruteos == null)
            {
                return NotFound();
            }
            var ruteo = await _context.Ruteos.FindAsync(RutCodigo);
            if (ruteo == null)
            {
                return NotFound();
            }

            _context.Ruteos.Remove(ruteo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuteoExists(int RutCodigo)
        {
            return (_context.Ruteos?.Any(e => e.RutCod == RutCodigo)).GetValueOrDefault();
        }
    }
}
