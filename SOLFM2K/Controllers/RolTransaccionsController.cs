using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SQLitePCL;

namespace SOLFM2K.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
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
          if (_context.RolTransaccions == null)
          {
              return NotFound();
          }
            return await _context.RolTransaccions.ToListAsync();
        }

        //devuelve las transacciones que tiene disponibles el rol consultado
        // GET: api/RolTransaccions/RtRol/{rtRol}
        [HttpGet("GetTransaccionbyRol")]
        public async Task<ActionResult<IEnumerable<RolTransaccion>>> GetRolTransaccionByRtRol(int rtRol)
        {
            if (_context.RolTransaccions == null)
            {
                return NotFound();
            }

            var rolTransacciones = await _context.RolTransaccions.Where(r => r.RtRol == rtRol).ToListAsync();

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
          if (_context.RolTransaccions == null)
          {
              return NotFound();
          }
            var rolTransaccion = await _context.RolTransaccions.FindAsync(id);

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
            if (rolTransaccion == null)
            {
                return BadRequest("La solicitud no contiene datos válidos");
            }

            // Verificar si ya existe un registro con la misma combinación de RtRol y RtTransaccion
            var existeRegistro = _context.RolTransaccions.Any(aut => aut.RtRol == rolTransaccion.RtRol && aut.RtTransaccion == rolTransaccion.RtTransaccion);
            if (existeRegistro)
            {
                return Conflict("El elemento ya existe");
            }
            else
            {
                try
                {
                    _context.RolTransaccions.Add(rolTransaccion);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(PostRolTransaccion), new { id = rolTransaccion.RtId }, rolTransaccion);
                }
                catch (DbUpdateException)
                {
                    // Manejar error al guardar en la base de datos
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar en la base de datos");
                }
            }


        }

        // DELETE: api/RolTransaccions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolTransaccion(int id)
        {
            if (_context.RolTransaccions == null)
            {
                return NotFound();
            }
            var rolTransaccion = await _context.RolTransaccions.FindAsync(id);
            if (rolTransaccion == null)
            {
                return NotFound();
            }

            _context.RolTransaccions.Remove(rolTransaccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolTransaccionExists(int id)
        {
            return (_context.RolTransaccions?.Any(e => e.RtId == id)).GetValueOrDefault();
        }
    }
}
