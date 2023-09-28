using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using System.Text.Json;
using Microsoft.OpenApi.Any;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SOLFM2K.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    
    public class CabSolCotizacionsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public CabSolCotizacionsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/CabSolCotizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CabSolCotizacion>>> GetCabSolCotizacions()
        {
          if (_context.CabSolCotizacions == null)
          {
              return NotFound();
          }
            return await _context.CabSolCotizacions.ToListAsync();
        }

        // GET: api/CabSolCotizacions/5
        [HttpGet("GetCabecerabyNomina")]
        public async Task<ActionResult<IEnumerable<CabSolCotizacion>>> GetCabecerabyNomina(int idNomina)
        {
            var cabSolCotizaciones = await _context.CabSolCotizacions.Where(c => c.CabSolCotIdEmisor == idNomina).ToListAsync();

            if (cabSolCotizaciones == null || cabSolCotizaciones.Count == 0)
            {
                return NotFound();
            }

            return cabSolCotizaciones;
        }


        [HttpGet("GetCabecerabyarea")]
        public async Task<ActionResult<IEnumerable<CabSolCotizacion>>> GetCabecerabyArea(int area)
        {
            var cabSolCotizaciones = await _context.CabSolCotizacions.Where(c => c.CabSolCotArea == area).ToListAsync();

            if (cabSolCotizaciones == null || cabSolCotizaciones.Count == 0)
            {
                return NotFound();
            }

            return cabSolCotizaciones;
        }



        [HttpGet("GetSolicitudByID")]
        public async Task<ActionResult<CotizacionTemplate>> getSolicitudByID(int ID)
        {
            // Obtener la cabecera de la solicitud
            var cabecera = await _context.CabSolCotizacions
                .FirstOrDefaultAsync(c => c.CabSolCotID == ID);

            if (cabecera == null)
            {
                return NotFound();
            }

            // Obtener detalles e items de la solicitud
            var detalles = await _context.DetSolCotizacions
                .Where(d => d.SolCotTipoSol == cabecera.CabSolCotTipoSolicitud && d.SolCotNoSol== cabecera.CabSolCotNoSolicitud)
                .ToListAsync();

            var solicitudCompleta = new CotizacionTemplate
            {
                Cabecera = cabecera,
                Detalles = detalles
            };

            solicitudCompleta.Items = new List<ItemSector>();

            foreach (var detalle in detalles)
            {
                var itemsDetalle = await _context.ItemSectores
                    .Where(i => i.ItmTipoSol == cabecera.CabSolCotTipoSolicitud &&
                                i.ItmNumSol == cabecera.CabSolCotNoSolicitud &&
                                i.ItmIdDetalle == detalle.SolCotIdDetalle)
                    .ToListAsync();

                solicitudCompleta.Items.AddRange(itemsDetalle);
            }

            return solicitudCompleta;
        }


        //[HttpGet("GetCabecerabyID")]
        //public async Task<ActionResult<IEnumerable<CabSolCotizacion>>> GetCabecerabyID(int tipoSol, int noSol)
        //{
        //    // Llamada al procedimiento almacenado mediante Entity Framework Core
        //    var result = await _context.CabSolCotizacions.FromSqlRaw("EXEC sp_GetIdSolicitud @p0, @p1", tipoSol, noSol).ToListAsync();

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return result;
        //}

        // PUT: api/CabSolCotizacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabSolCotizacion(int id, CabSolCotizacion cabSolCotizacion)
        {
            if (id != cabSolCotizacion.CabSolCotID)
            {
                return BadRequest();
            }

            _context.Entry(cabSolCotizacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabSolCotizacionExists(id))
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

        // POST: api/CabSolCotizacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CabSolCotizacion>> PostCabSolCotizacion(CabSolCotizacion cabSolCotizacion)
        {
          if (_context.CabSolCotizacions == null)
          {
              return Problem("Entity set 'SolicitudContext.CabSolCotizacions'  is null.");
          }
            _context.CabSolCotizacions.Add(cabSolCotizacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCabSolCotizacion), new { id = cabSolCotizacion.CabSolCotNoSolicitud }, cabSolCotizacion);
        }

        // DELETE: api/CabSolCotizacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabSolCotizacion(int id)
        {
            if (_context.CabSolCotizacions == null)
            {
                return NotFound();
            }
            var cabSolCotizacion = await _context.CabSolCotizacions.FindAsync(id);
            if (cabSolCotizacion == null)
            {
                return NotFound();
            }

            _context.CabSolCotizacions.Remove(cabSolCotizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CabSolCotizacionExists(int id)
        {
            return (_context.CabSolCotizacions?.Any(e => e.CabSolCotNoSolicitud == id)).GetValueOrDefault();
        }
    }
}
