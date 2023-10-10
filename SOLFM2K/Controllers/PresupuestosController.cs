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
    public class PresupuestosController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public PresupuestosController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Presupuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presupuesto>>> GetPresupuestos()
        {
          if (_context.Presupuestos == null)
          {
              return NotFound();
          }
            return await _context.Presupuestos.ToListAsync();
        }

        // GET: api/Presupuestos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> GetPresupuesto(int id)
        {
          if (_context.Presupuestos == null)
          {
              return NotFound();
          }
            var presupuesto = await _context.Presupuestos.FindAsync(id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return presupuesto;
        }

        // PUT: api/Presupuestos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresupuesto(int id, Presupuesto presupuesto)
        {
            if (id != presupuesto.PrespId)
            {
                return BadRequest();
            }

            _context.Entry(presupuesto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresupuestoExists(id))
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

        // POST: api/Presupuestos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Presupuesto>> PostPresupuesto(Presupuesto presupuesto)
        {
          if (_context.Presupuestos == null)
          {
              return Problem("Entity set 'SolicitudContext.Presupuestos'  is null.");
          }
            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPresupuesto", new { id = presupuesto.PrespId }, presupuesto);
        }

        // DELETE: api/Presupuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresupuesto(int id)
        {
            if (_context.Presupuestos == null)
            {
                return NotFound();
            }
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PresupuestoExists(int id)
        {
            return (_context.Presupuestos?.Any(e => e.PrespId == id)).GetValueOrDefault();
        }
    }
}
