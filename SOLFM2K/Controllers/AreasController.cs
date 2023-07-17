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
    public class AreasController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public AreasController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetArea()
        {
          if (_context.Area == null)
          {
              return NotFound();
          }
            return await _context.Area.ToListAsync();
        }

        // GET: api/Areas/5
        [HttpGet("{AreaId}")]
        public async Task<ActionResult<Area>> GetArea(int AreaId)
        {
          if (_context.Area == null)
          {
              return NotFound();
          }
            var area = await _context.Area.FindAsync(AreaId);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{AreaId}")]
        public async Task<IActionResult> PutArea(int AreaId, Area area)
        {
            if (AreaId != area.AreaId)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(AreaId))
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

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
          if (_context.Area == null)
          {
              return Problem("Entity set 'SolicitudContext.Area'  is null.");
          }
            _context.Area.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostArea), new { id = area.AreaId }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{AreaId}")]
        public async Task<IActionResult> DeleteArea(int AreaId)
        {
            if (_context.Area == null)
            {
                return NotFound();
            }
            var area = await _context.Area.FindAsync(AreaId);
            if (area == null)
            {
                return NotFound();
            }

            _context.Area.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaExists(int AreaId)
        {
            return (_context.Area?.Any(e => e.AreaId == AreaId)).GetValueOrDefault();
        }
    }
}
