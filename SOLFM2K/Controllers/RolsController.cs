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
        [HttpGet("{RoCodigo}")]
        public async Task<ActionResult<Rol>> GetRol(int RoCodigo)
        {
          if (_context.Rols == null)
          {
              return NotFound();
          }
            var rol = await _context.Rols.FindAsync(RoCodigo);

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
            if (RoCodigo != rol.RoCodigo)
            {
                return BadRequest();
            }

            _context.Entry(rol).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(RoCodigo))
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
        [HttpDelete("{RoCodigo}")]
        public async Task<IActionResult> DeleteRol(int RoCodigo)
        {
            if (_context.Rols == null)
            {
                return NotFound();
            }
            var rol = await _context.Rols.FindAsync(RoCodigo);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int RoCodigo)
        {
            return (_context.Rols?.Any(e => e.RoCodigo == RoCodigo)).GetValueOrDefault();
        }
    }
}
