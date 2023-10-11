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
    public class NivGerenciumsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public NivGerenciumsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/NivGerenciums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NivGerencium>>> GetNivGerencia()
        {
          if (_context.NivGerencia == null)
          {
              return NotFound();
          }
            return await _context.NivGerencia.ToListAsync();
        }

        // GET: api/NivGerenciums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NivGerencium>> GetNivGerencium(int id)
        {
          if (_context.NivGerencia == null)
          {
              return NotFound();
          }
            var nivGerencium = await _context.NivGerencia.FindAsync(id);

            if (nivGerencium == null)
            {
                return NotFound();
            }

            return nivGerencium;
        }

        // PUT: api/NivGerenciums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNivGerencium(int id, NivGerencium nivGerencium)
        {
            if (id != nivGerencium.GerNivId)
            {
                return BadRequest();
            }

            _context.Entry(nivGerencium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivGerenciumExists(id))
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

        // POST: api/NivGerenciums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NivGerencium>> PostNivGerencium(NivGerencium nivGerencium)
        {
          if (_context.NivGerencia == null)
          {
              return Problem("Entity set 'SolicitudContext.NivGerencia'  is null.");
          }
            _context.NivGerencia.Add(nivGerencium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNivGerencium", new { id = nivGerencium.GerNivId }, nivGerencium);
        }

        // DELETE: api/NivGerenciums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNivGerencium(int id)
        {
            if (_context.NivGerencia == null)
            {
                return NotFound();
            }
            var nivGerencium = await _context.NivGerencia.FindAsync(id);
            if (nivGerencium == null)
            {
                return NotFound();
            }

            _context.NivGerencia.Remove(nivGerencium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NivGerenciumExists(int id)
        {
            return (_context.NivGerencia?.Any(e => e.GerNivId == id)).GetValueOrDefault();
        }
    }
}
