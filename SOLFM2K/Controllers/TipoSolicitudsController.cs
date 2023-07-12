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
    public class TipoSolicitudsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public TipoSolicitudsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/TipoSolicituds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSolicitud>>> GetTipoSolicituds()
        {
          if (_context.TipoSolicituds == null)
          {
              return NotFound();
          }
            return await _context.TipoSolicituds.ToListAsync();
        }

        // GET: api/TipoSolicituds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSolicitud>> GetTipoSolicitud(int id)
        {
          if (_context.TipoSolicituds == null)
          {
              return NotFound();
          }
            var tipoSolicitud = await _context.TipoSolicituds.FindAsync(id);

            if (tipoSolicitud == null)
            {
                return NotFound();
            }

            return tipoSolicitud;
        }

        // PUT: api/TipoSolicituds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoSolicitud(int id, TipoSolicitud tipoSolicitud)
        {
            if (id != tipoSolicitud.TipoSolId)
            {
                return BadRequest();
            }

            _context.Entry(tipoSolicitud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoSolicitudExists(id))
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

        // POST: api/TipoSolicituds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoSolicitud>> PostTipoSolicitud(TipoSolicitud tipoSolicitud)
        {
          if (_context.TipoSolicituds == null)
          {
              return Problem("Entity set 'SolicitudContext.TipoSolicituds'  is null.");
          }
            _context.TipoSolicituds.Add(tipoSolicitud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoSolicitud", new { id = tipoSolicitud.TipoSolId }, tipoSolicitud);
        }

        // DELETE: api/TipoSolicituds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoSolicitud(int id)
        {
            if (_context.TipoSolicituds == null)
            {
                return NotFound();
            }
            var tipoSolicitud = await _context.TipoSolicituds.FindAsync(id);
            if (tipoSolicitud == null)
            {
                return NotFound();
            }

            _context.TipoSolicituds.Remove(tipoSolicitud);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoSolicitudExists(int id)
        {
            return (_context.TipoSolicituds?.Any(e => e.TipoSolId == id)).GetValueOrDefault();
        }
    }
}
