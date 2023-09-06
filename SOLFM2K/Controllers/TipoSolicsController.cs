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
    public class TipoSolicsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public TipoSolicsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/TipoSolics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSolic>>> GetTipoSolics()
        {
          if (_context.TipoSolics == null)
          {
              return NotFound();
          }
            return await _context.TipoSolics.ToListAsync();
        }

        // GET: api/TipoSolics/5
        [HttpGet("{TipoSolId}")]
        public async Task<ActionResult<TipoSolic>> GetTipoSolic(int TipoSolId)
        {
          if (_context.TipoSolics == null)
          {
              return NotFound();
          }
            var tipoSolic = await _context.TipoSolics.FindAsync(TipoSolId);

            if (tipoSolic == null)
            {
                return NotFound();
            }

            return tipoSolic;
        }

        // PUT: api/TipoSolics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{TipoSolId}")]
        public async Task<IActionResult> PutTipoSolic(int TipoSolId, TipoSolic tipoSolic)
        {
            if (TipoSolId != tipoSolic.TipoSolId)
            {
                return BadRequest();
            }

            _context.Entry(tipoSolic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoSolicExists(TipoSolId))
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

        // POST: api/TipoSolics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoSolic>> PostTipoSolic(TipoSolic tipoSolic)
        {
          if (_context.TipoSolics == null)
          {
              return Problem("Entity set 'SolicitudContext.TipoSolics'  is null.");
          }
            _context.TipoSolics.Add(tipoSolic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTipoSolic), new { TipoSolId = tipoSolic.TipoSolId }, tipoSolic);
        }

        // DELETE: api/TipoSolics/5
        [HttpDelete("{TipoSolId}")]
        public async Task<IActionResult> DeleteTipoSolic(int TipoSolId)
        {
            if (_context.TipoSolics == null)
            {
                return NotFound();
            }
            var tipoSolic = await _context.TipoSolics.FindAsync(TipoSolId);
            if (tipoSolic == null)
            {
                return NotFound();
            }

            _context.TipoSolics.Remove(tipoSolic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoSolicExists(int TipoSolId)
        {
            return (_context.TipoSolics?.Any(e => e.TipoSolId == TipoSolId)).GetValueOrDefault();
        }
    }
}
