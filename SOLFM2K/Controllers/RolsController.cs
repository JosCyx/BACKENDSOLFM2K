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
    public class RolsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public RolsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Rols
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRols()
        {
          if (_context.Rols == null)
          {
              return NotFound();
          }
            return await _context.Rols.ToListAsync();
        }

        // GET: api/Rols/5
        [HttpGet("{Rocodigo}")]
        public async Task<ActionResult<Rol>> GetRol(short Rocodigo)
        {
          if (_context.Rols == null)
          {
              return NotFound();
          }
            var rol = await _context.Rols.FindAsync(Rocodigo);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        // PUT: api/Rols/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RoCodigo}")]
        public async Task<IActionResult> PutRol(short RoCodigo, Rol rol)
        {
            try
            {
                if(RoCodigo != rol.RoCodigo)
                {
                    return BadRequest();
                }
                var rolItem = await _context.Rols.FindAsync(RoCodigo);
                if (rolItem == null)
                {
                    return NotFound();
                }
                rolItem.RoNombre = rol.RoNombre;
                rolItem.RoEstado = rol.RoEstado;



                await _context.SaveChangesAsync();  
                return NotFound();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }

        }

        // POST: api/Rols
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rol>> PostRol(Rol rol)
        {
          if (_context.Rols == null)
          {
              return Problem("Entity set 'SolicitudContext.Rols'  is null.");
          }
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRol), new { id = rol.RoCodigo }, rol);
        }

        // DELETE: api/Rols/5
        [HttpDelete("{Rocodigo}")]
        public async Task<IActionResult> DeleteRol(short Rocodigo)
        {
            if (_context.Rols == null)
            {
                return NotFound();
            }
            var rol = await _context.Rols.FindAsync(Rocodigo);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(short id)
        {
            return (_context.Rols?.Any(e => e.RoCodigo == id)).GetValueOrDefault();
        }
    }
}
