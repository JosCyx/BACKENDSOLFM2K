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
    public class RolTransaccionsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public RolTransaccionsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/RolTransaccions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolTransaccion>>> GetRolTransaccion()
        {
          if (_context.RolTransaccion == null)
          {
              return NotFound();
          }
            return await _context.RolTransaccion.ToListAsync();
        }

        //devuelve las transacciones que tiene disponibles el rol consultado
        // GET: api/RolTransaccions/RtRol/{rtRol}
        [HttpGet("GetTransaccionbyRol")]
        public async Task<ActionResult<IEnumerable<RolTransaccion>>> GetRolTransaccionByRtRol(int rtRol)
        {
            if (_context.RolTransaccion == null)
            {
                return NotFound();
            }

            var rolTransacciones = await _context.RolTransaccion.Where(r => r.RtRol == rtRol).ToListAsync();

            if (rolTransacciones == null || !rolTransacciones.Any())
            {
                return NotFound();
            }

            return rolTransacciones;
        }




        // GET: api/RolTransaccions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolTransaccion>> GetRolTransaccion(int id)
        {
          if (_context.RolTransaccion == null)
          {
              return NotFound();
          }
            var rolTransaccion = await _context.RolTransaccion.FindAsync(id);

            if (rolTransaccion == null)
            {
                return NotFound();
            }

            return rolTransaccion;
        }

        // PUT: api/RolTransaccions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolTransaccion(int id, RolTransaccion rolTransaccion)
        {
            if (id != rolTransaccion.RtId)
            {
                return BadRequest();
            }

            _context.Entry(rolTransaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolTransaccionExists(id))
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

        // POST: api/RolTransaccions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RolTransaccion>> PostRolTransaccion(RolTransaccion rolTransaccion)
        {
          if (_context.RolTransaccion == null)
          {
              return Problem("Entity set 'SolicitudContext.RolTransaccion'  is null.");
          }
            _context.RolTransaccion.Add(rolTransaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRolTransaccion), new { id = rolTransaccion.RtId }, rolTransaccion);
        }

        // DELETE: api/RolTransaccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolTransaccion(int id)
        {
            if (_context.RolTransaccion == null)
            {
                return NotFound();
            }
            var rolTransaccion = await _context.RolTransaccion.FindAsync(id);
            if (rolTransaccion == null)
            {
                return NotFound();
            }

            _context.RolTransaccion.Remove(rolTransaccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolTransaccionExists(int id)
        {
            return (_context.RolTransaccion?.Any(e => e.RtId == id)).GetValueOrDefault();
        }
    }
}
