using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

namespace SOLFM2K.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class SolTrackingsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public SolTrackingsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/SolTrackings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolTracking>>> GetSolTrackings()
        {
          if (_context.SolTrackings == null)
          {
              return NotFound();
          }
            return await _context.SolTrackings.ToListAsync();
        }

        //obtiene el ultimo numero de solicitud registrada segun el tipo de solicitud ingresado
        [HttpGet("GetLastSol")]
        public async Task<ActionResult<IEnumerable<SolTracking>>> GetLastSolicitud(int tipoSol)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var result = await _context.SolTrackings.FromSqlRaw("EXEC sp_GetLastTrackingValue @p0", tipoSol).ToListAsync();
            
            if (result.Count == 0)
            {
                return Ok(0);
            }
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }



        // GET: api/SolTrackings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SolTracking>> GetSolTracking(int id)
        {
          if (_context.SolTrackings == null)
          {
              return NotFound();
          }
            var solTracking = await _context.SolTrackings.FindAsync(id);

            if (solTracking == null)
            {
                return NotFound();
            }

            return solTracking;
        }

        // PUT: api/SolTrackings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolTracking(int id, SolTracking solTracking)
        {
            if (id != solTracking.SolTrId)
            {
                return BadRequest();
            }

            _context.Entry(solTracking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolTrackingExists(id))
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

        // POST: api/SolTrackings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SolTracking>> PostSolTracking(SolTracking solTracking)
        {
          if (_context.SolTrackings == null)
          {
              return Problem("Entity set 'SolicitudContext.SolTrackings'  is null.");
          }
            _context.SolTrackings.Add(solTracking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostSolTracking), new { id = solTracking.SolTrId }, solTracking);
        }

        // DELETE: api/SolTrackings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolTracking(int id)
        {
            if (_context.SolTrackings == null)
            {
                return NotFound();
            }
            var solTracking = await _context.SolTrackings.FindAsync(id);
            if (solTracking == null)
            {
                return NotFound();
            }

            _context.SolTrackings.Remove(solTracking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SolTrackingExists(int id)
        {
            return (_context.SolTrackings?.Any(e => e.SolTrId == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteSolTrackingBySolId")]
        public async Task<IActionResult> DeleteSolTrackingBySolId(int tipoSol, int noSol, int idTracking)
        {
            if (_context.SolTrackings == null)
            {
                return NotFound();
            }
            var solTracking = await _context.SolTrackings.Where(x => x.SolTrId == idTracking && x.SolTrTipoSol == tipoSol && x.SolTrNumSol == noSol).ToListAsync();
            if (solTracking == null)
            {
                return NotFound();
            }

            _context.SolTrackings.RemoveRange(solTracking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
