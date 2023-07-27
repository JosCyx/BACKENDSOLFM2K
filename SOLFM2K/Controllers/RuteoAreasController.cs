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
    public class RuteoAreasController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public RuteoAreasController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/RuteoAreas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuteoArea>>> GetRuteoAreas()
        {
          if (_context.RuteoAreas == null)
          {
              return NotFound();
          }
            return await _context.RuteoAreas.ToListAsync();
        }

        // GET: api/RuteoAreas/5
        //get que busca ruteos segun el area
        [HttpGet("{RutareaArea}")]
        public async Task<ActionResult<IEnumerable<RuteoArea>>> GetRuteoAreasByRutaArea(int RutareaArea)
        {
            var ruteoAreas = await _context.RuteoAreas.Where(ra => ra.RutareaArea == RutareaArea).ToListAsync();

            if (ruteoAreas == null || ruteoAreas.Count == 0)
            {
                return NotFound();
            }

            return ruteoAreas;
        }

        //get que busca areas por id
        /*public async Task<ActionResult<RuteoArea>> GetRuteoArea(int RutareaId)
        {
          if (_context.RuteoAreas == null)
          {
              return NotFound();
          }
            var ruteoArea = await _context.RuteoAreas.FindAsync(RutareaId);
            

            if (ruteoArea == null)
            {
                return NotFound();
            }

            return ruteoArea;

        }*/



        // PUT: api/RuteoAreas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RutareaId}")]
        public async Task<IActionResult> PutRuteoArea(int RutareaId, RuteoArea ruteoArea)
        {
            if (RutareaId != ruteoArea.RutareaId)
            {
                return BadRequest();
            }

            _context.Entry(ruteoArea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuteoAreaExists(RutareaId))
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

        // POST: api/RuteoAreas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuteoArea>> PostRuteoArea(RuteoArea ruteoArea)
        {
          if (_context.RuteoAreas == null)
          {
              return Problem("Entity set 'SolicitudContext.RuteoAreas'  is null.");
          }
            _context.RuteoAreas.Add(ruteoArea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRuteoArea), new { RutareaId = ruteoArea.RutareaId }, ruteoArea);
        }

        // DELETE: api/RuteoAreas/5
        [HttpDelete("{RutareaTipoSol},{RutareaArea},{RutareaNivel}")]
        public async Task<IActionResult> DeleteRuteoArea(int RutareaTipoSol, int RutareaArea, int RutareaNivel)
        {
            if (_context.RuteoAreas == null)
            {
                return NotFound();
            }

            var ruteoArea = await _context.RuteoAreas
                .FirstOrDefaultAsync(ra => ra.RutareaTipoSol == RutareaTipoSol && ra.RutareaArea == RutareaArea && ra.RutareaNivel == RutareaNivel);

            if (ruteoArea == null)
            {
                return NotFound();
            }

            _context.RuteoAreas.Remove(ruteoArea);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /*[HttpDelete("{RutareaId}")]
        public async Task<IActionResult> DeleteRuteoArea(int RutareaId)
        {
            if (_context.RuteoAreas == null)
            {
                return NotFound();
            }
            var ruteoArea = await _context.RuteoAreas.FindAsync(RutareaId);
            if (ruteoArea == null)
            {
                return NotFound();
            }

            _context.RuteoAreas.Remove(ruteoArea);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool RuteoAreaExists(int RutareaId)
        {
            return (_context.RuteoAreas?.Any(e => e.RutareaId == RutareaId)).GetValueOrDefault();
        }

        // GET: api/RuteoAreas/checkRuteoExistence
        [HttpGet("checkRuteoExistence")]
        public async Task<ActionResult<bool>> CheckRuteoExistence(int rutTipoSol, int rutArea, int rutNivel)
        {
            try
            {
                // Busca el ruteo en la base de datos que coincida con los parámetros proporcionados
                var existingRuteo = await _context.RuteoAreas
                    .FirstOrDefaultAsync(ra => ra.RutareaTipoSol == rutTipoSol && ra.RutareaArea == rutArea && ra.RutareaNivel == rutNivel);

                if (existingRuteo != null)
                {
                    // Si el ruteo ya existe, retornas true
                    return true;
                }
                else
                {
                    // Si el ruteo no existe, retornas false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // En caso de error, retornas un código de estado 500 y un mensaje de error
                return StatusCode(500, $"Error al verificar la existencia del ruteo: {ex.Message}");
            }
        }
    }
}
