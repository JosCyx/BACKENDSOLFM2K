﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SOLFM2K.Models;

namespace SOLFM2K.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class DetSolCotizacionsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public DetSolCotizacionsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/DetSolCotizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetSolCotizacion>>> GetDetSolCotizacions()
        {
          if (_context.DetSolCotizacions == null)
          {
              return NotFound();
          }
            return await _context.DetSolCotizacions.ToListAsync();
        }
        //Procedimiento de GetDetalle_solicitud
        [HttpGet("GetDetalleSolicitud")]
        public async Task<ActionResult<IEnumerable<DetSolCotizacion>>> GetDetSolCotizacions(int tipoSol, int noSol)
        {
            var result = await _context.DetSolCotizacions.FromSqlRaw("EXEC sp_GetDetalle_solicitud @p0, @p1", tipoSol, noSol).ToListAsync();
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

        // GET: api/DetSolCotizacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetSolCotizacion>> GetDetSolCotizacion(int id)
        {
          if (_context.DetSolCotizacions == null)
          {
              return NotFound();
          }
            var detSolCotizacion = await _context.DetSolCotizacions.FindAsync(id);

            if (detSolCotizacion == null)
            {
                return NotFound();
            }

            return detSolCotizacion;
        }

        [HttpGet("GetLastDetalleCot")]
        public async Task<ActionResult<IEnumerable<DetSolCotizacion>>> GetLastDetalleCot(int tipoSol, int noSol)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var result = await _context.DetSolCotizacions.FromSqlRaw("EXEC sp_GetLastDetallebyCotId @p0, @p1", tipoSol, noSol).ToListAsync();

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

        [HttpGet("GetLastDetalleID")]
        public async Task<ActionResult<IEnumerable<DetSolCotizacion>>> GetLastItemID()
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var result = await _context.DetSolCotizacions.FromSqlRaw("EXEC getLastID ").ToListAsync();

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


        // PUT: api/DetSolCotizacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetSolCotizacion(int id, DetSolCotizacion detSolCotizacion)
        {
            if (id != detSolCotizacion.SolCotIdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(detSolCotizacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetSolCotizacionExists(id))
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

        // POST: api/DetSolCotizacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetSolCotizacion>> PostDetSolCotizacion(DetSolCotizacion detSolCotizacion)
        {
          if (_context.DetSolCotizacions == null)
          {
              return Problem("Entity set 'SolicitudContext.DetSolCotizacions'  is null.");
          }
            _context.DetSolCotizacions.Add(detSolCotizacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDetSolCotizacion), new { id = detSolCotizacion.SolCotIdDetalle }, detSolCotizacion);
        }

        //metodo que llama al SP en la base y elimina todos los detalles
        [HttpDelete("DeleteAllDetails")]
        public async Task<IActionResult> DeleteAllItembySol(int tipoSol, int noSol)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC deleteDetallesbySol @tipoSol, @noSol",
                    new SqlParameter("@tipoSol", tipoSol),
                    new SqlParameter("@noSol", noSol));

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting items.");
            }
        }

        // DELETE: api/DetSolCotizacions/5
        [HttpDelete("{SolCotID}")]
        public async Task<IActionResult> DeleteDetSolCotizacion(int SolCotID)
        {
            if (_context.DetSolCotizacions == null)
            {
                return NotFound();
            }
            var detSolCotizacion = await _context.DetSolCotizacions.FindAsync(SolCotID);
            if (detSolCotizacion == null)
            {
                return NotFound();
            }

            _context.DetSolCotizacions.Remove(detSolCotizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetSolCotizacionExists(int id)
        {
            return (_context.DetSolCotizacions?.Any(e => e.SolCotIdDetalle == id)).GetValueOrDefault();
        }
    }
}
