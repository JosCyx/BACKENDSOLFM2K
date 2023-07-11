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
    public class PruebasController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public PruebasController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Pruebas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prueba>>> GetPruebas()
        {
          if (_context.Pruebas == null)
          {
              return NotFound();
          }
            return await _context.Pruebas.ToListAsync();
        }

        // GET: api/Pruebas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prueba>> GetPrueba(int id)
        {
          if (_context.Pruebas == null)
          {
              return NotFound();
          }
            var prueba = await _context.Pruebas.FindAsync(id);

            if (prueba == null)
            {
                return NotFound();
            }

            return prueba;
        }

        // PUT: api/Pruebas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrueba(int id, Prueba prueba)
        {
            if (id != prueba.Id)
            {
                return BadRequest();
            }

            _context.Entry(prueba).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PruebaExists(id))
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

        // POST: api/Pruebas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prueba>> PostPrueba(Prueba prueba)
        {
          if (_context.Pruebas == null)
          {
              return Problem("Entity set 'SolicitudContext.Pruebas'  is null.");
          }
            _context.Pruebas.Add(prueba);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrueba", new { id = prueba.Id }, prueba);
        }

        // DELETE: api/Pruebas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrueba(int id)
        {
            if (_context.Pruebas == null)
            {
                return NotFound();
            }
            var prueba = await _context.Pruebas.FindAsync(id);
            if (prueba == null)
            {
                return NotFound();
            }

            _context.Pruebas.Remove(prueba);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PruebaExists(int id)
        {
            return (_context.Pruebas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
