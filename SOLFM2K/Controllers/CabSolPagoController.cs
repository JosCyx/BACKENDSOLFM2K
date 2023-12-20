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
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class CabSolPagoController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public CabSolPagoController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/CabSolPagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CabSolPago>>> GetCabSolPagos()
        {
          if (_context.CabSolPagos == null)
          {
              return NotFound();
          }
            return await _context.CabSolPagos.ToListAsync();
        }

        // GET: api/CabSolCotizacions/5
        [HttpGet("GetCabecerabyNomina")]
        public async Task<ActionResult<IEnumerable<CabSolPago>>> GetCabecerabyNomina(string idNomina)
        {
            var cabSolCotizaciones = await _context.CabSolPagos.Where(c => c.CabPagoIdEmisor == idNomina).ToListAsync();

            if (cabSolCotizaciones == null || cabSolCotizaciones.Count == 0)
            {
                return NotFound();
            }

            return cabSolCotizaciones;
        }


        [HttpGet("GetCabecerabyarea")]
        public async Task<ActionResult<IEnumerable<CabSolPago>>> GetCabecerabyArea(int area)
        {
            var cabSolCotizaciones = await _context.CabSolPagos.Where(c =>  c.CabPagoIdAreaSolicitante == area).ToListAsync();

            if (cabSolCotizaciones == null || cabSolCotizaciones.Count == 0)
            {
                return NotFound();
            }

            return cabSolCotizaciones;
        }


        // GET: api/CabSolPagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CabSolPago>> GetCabSolPago(int id)
        {
          if (_context.CabSolPagos == null)
          {
              return NotFound();
          }
            var cabSolPago = await _context.CabSolPagos.FindAsync(id);

            if (cabSolPago == null)
            {
                return NotFound();
            }

            return cabSolPago;
        }
        [HttpGet("GetSOEstado")]
        public async Task<ActionResult<IEnumerable<CabSolPago>>> GetOPEstado(string state)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var result = await _context.CabSolPagos.Where(ca => ca.CabPagoEstado == state).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("UpdateEstadoTracking")]
        public IActionResult UpdateEstadoTracking(int tipoSol, int noSol, int newEstado)
        {
            var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate. CabPagoEstadoTrack = newEstado;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateEstado")]
        public IActionResult UpdateEstado(int tipoSol, int noSol, string newEstado)
        {
            var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.CabPagoEstado = newEstado;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateAprobado")]
        public IActionResult UpdateAprobado(int tipoSol, int noSol, string id)
        {
            var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.CabPagoApprovedBy = id;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateFecha")]
        public IActionResult UpdateFecha(int tipoSol, int noSol, DateTime fecha)
        {
            var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.CabPagoFechaEmision = fecha;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateMotivoDevolucion")]
        public IActionResult UpdateMotivoDevolucion(int tipoSol, int noSol, string motivo)
        {
            var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.CabPagoMotivoDev = motivo;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateIfDestino")]
        public IActionResult UpdateIfDestino(int tipoSol, int noSol, string destino)
        {
            var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.CabPagoIfDestino = destino;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        //[HttpPut("UpdateFinanciero")]
        //public IActionResult UpdateFinanciero(int tipoSol, int noSol, string id)
        //{
        //    var entityToUpdate = _context.CabSolPagos.FirstOrDefault(e => e.CabPagoTipoSolicitud == tipoSol && e.CabPagoNoSolicitud == noSol);

        //    if (entityToUpdate == null)
        //    {
        //        return NotFound(); // Devuelve un código 404 si el registro no existe.
        //    }

        //    // Actualiza el valor del campo deseado en el objeto entityToUpdate.
        //    entityToUpdate.CabPagoFinancieroBy = id;

        //    // Guarda los cambios en la base de datos.
        //    _context.SaveChanges();

        //    return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        //}

        // PUT: api/CabSolPagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabSolPago(int id, CabSolPago cabSolPago)
        {
            if (id != cabSolPago.CabPagoID)
            {
                return BadRequest();
            }

            _context.Entry(cabSolPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabSolPagoExists(id))
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
        //get Cabecera y detalle 
        [HttpGet("GetSolicitudByID")]
        public async Task<ActionResult<PagoTemplate>> getSolicitudByID(int ID)
        {
            // Obtener la cabecera de la solicitud
            var cabecera = await _context.CabSolPagos
                .FirstOrDefaultAsync(c => c.CabPagoID == ID);

            if (cabecera == null)
            {
                return NotFound();
            }

            // Obtener detalles e items de la solicitud
            var detalles = await _context.DetSolPagos
                .Where(d => d.DetPagoTipoSol == cabecera.CabPagoTipoSolicitud && d.DetPagoNoSol == cabecera.CabPagoNoSolicitud)
                .ToListAsync();

            var solicitudCompleta = new PagoTemplate
            {
                cabecera = cabecera,
                detalles = detalles
            };


            
            return solicitudCompleta;
        }

        // POST: api/CabSolPagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CabSolPago>> PostCabSolPago(CabSolPago cabSolPago)
        {
          if (_context.CabSolPagos == null)
          {
              return Problem("Entity set 'SolicitudContext.CabSolPagos'  is null.");
          }
            _context.CabSolPagos.Add(cabSolPago);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CabSolPagoExists(cabSolPago.CabPagoID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostCabSolPago), new { id = cabSolPago.CabPagoID }, cabSolPago);
        }

        // DELETE: api/CabSolPagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabSolPago(int id)
        {
            if (_context.CabSolPagos == null)
            {
                return NotFound();
            }
            var cabSolPago = await _context.CabSolPagos.FindAsync(id);
            if (cabSolPago == null)
            {
                return NotFound();
            }

            _context.CabSolPagos.Remove(cabSolPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("DeletesSolOrdenPago")]
        public async Task<IActionResult> DeletesSolOrdenPago(int tipoSol, int noSol)
        {
            if (_context.CabSolPagos == null)
            {
                return NotFound();
            }
            var cabSolOrdenPago = await _context.CabSolPagos.FirstOrDefaultAsync(c => c.CabPagoTipoSolicitud == tipoSol && c.CabPagoNoSolicitud == noSol);
            if (cabSolOrdenPago == null)
            {
                return NotFound();
            }
            if (cabSolOrdenPago.CabPagoValido == 1)
            {
                cabSolOrdenPago.CabPagoValido = 0; // Cambiar el valor a 0
                _context.Entry(cabSolOrdenPago).State = EntityState.Modified;
            }
            //_context.CabSolPagos.Remove(cabSolOrdenPago);
            //_context.DetSolPagos.RemoveRange(detalles);
            //_context.Documentos.RemoveRange(documentos);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool CabSolPagoExists(int id)
        {
            return (_context.CabSolPagos?.Any(e => e.CabPagoID == id)).GetValueOrDefault();
        }
    }
}
