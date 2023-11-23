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
    public class CabSolOrdenComprasController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public CabSolOrdenComprasController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/CabSolOrdenCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CabSolOrdenCompra>>> GetCabSolOrdenCompras()
        {
          if (_context.CabSolOrdenCompras == null)
          {
              return NotFound();
          }
            return await _context.CabSolOrdenCompras.ToListAsync();
        }

        // GET: api/CabSolCotizacions/5
        [HttpGet("GetCabecerabyNomina")]
        public async Task<ActionResult<IEnumerable<CabSolOrdenCompra>>> GetCabecerabyNomina(string idNomina)
        {
            var cabSolCotizaciones = await _context.CabSolOrdenCompras.Where(c => c.cabSolOCIdEmisor == idNomina).ToListAsync();

            if (cabSolCotizaciones == null || cabSolCotizaciones.Count == 0)
            {
                return NotFound();
            }

            return cabSolCotizaciones;
        }


        [HttpGet("GetCabecerabyarea")]
        public async Task<ActionResult<IEnumerable<CabSolOrdenCompra>>> GetCabecerabyArea(int area)
        {
            var cabSolCotizaciones = await _context.CabSolOrdenCompras.Where(c =>c.cabSolOCIdArea == area).ToListAsync();

            if (cabSolCotizaciones == null || cabSolCotizaciones.Count == 0)
            {
                return NotFound();
            }

            return cabSolCotizaciones;
        }


        [HttpGet("GetSolicitudByID")]
        public async Task<ActionResult<OrdenComprasTemplate>> getSolicitudByID(int ID)
        {
            // Obtener la cabecera de la solicitud
            var cabecera = await _context.CabSolOrdenCompras
                .FirstOrDefaultAsync(c => c.cabSolOCID == ID);

            if (cabecera == null)
            {
                return NotFound();
            }

            // Obtener detalles e items de la solicitud
            var detalles = await _context.DetSolCotizacions
                .Where(d => d.SolCotTipoSol == cabecera.cabSolOCTipoSolicitud && d.SolCotNoSol == cabecera.cabSolOCNoSolicitud)
                .ToListAsync();

            var solicitudCompleta = new OrdenComprasTemplate
            {
                Cabecera = cabecera,
                Detalles = detalles
            };

            solicitudCompleta.Items = new List<ItemSector>();

            foreach (var detalle in detalles)
            {
                var itemsDetalle = await _context.ItemSectores
                    .Where(i => i.ItmTipoSol == cabecera.cabSolOCTipoSolicitud &&
                                i.ItmNumSol == cabecera.cabSolOCNoSolicitud &&
                                i.ItmIdDetalle == detalle.SolCotIdDetalle)
                    .ToListAsync();

                solicitudCompleta.Items.AddRange(itemsDetalle);
            }

            return solicitudCompleta;
        }
        [HttpGet("GetOCEstado")]
        public async Task<ActionResult<IEnumerable<CabSolOrdenCompra>>> GetOCEstado(string state)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var result = await _context.CabSolOrdenCompras.Where(ca => ca.cabSolOCEstado == state).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPut("UpdateMotivoDevolucion")]
        public IActionResult UpdateMotivoDevolucion(int tipoSol, int noSol, string motivo)
        {
            var entityToUpdate = _context.CabSolOrdenCompras.FirstOrDefault(e => e.cabSolOCTipoSolicitud == tipoSol && e.cabSolOCNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.cabSolOCMotivoDev = motivo;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateEstadoTracking")]
        public IActionResult UpdateEstadoTracking(int tipoSol, int noSol, int newEstado)
        {
            var entityToUpdate = _context.CabSolOrdenCompras.FirstOrDefault(e => e.cabSolOCTipoSolicitud == tipoSol && e.cabSolOCNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.cabSolOCEstadoTracking = newEstado;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateEstado")]
        public IActionResult UpdateEstado(int tipoSol, int noSol, string newEstado)
        {
            var entityToUpdate = _context.CabSolOrdenCompras.FirstOrDefault(e => e.cabSolOCTipoSolicitud == tipoSol && e.cabSolOCNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.cabSolOCEstado = newEstado;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateAprobado")]
        public IActionResult UpdateAprobado(int tipoSol, int noSol, string id)
        {
            var entityToUpdate = _context.CabSolOrdenCompras.FirstOrDefault(e => e.cabSolOCTipoSolicitud == tipoSol && e.cabSolOCNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.cabSolOCApprovedBy = id;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        [HttpPut("UpdateFinanciero")]
        public IActionResult UpdateFinanciero(int tipoSol, int noSol, string id)
        {
            var entityToUpdate = _context.CabSolOrdenCompras.FirstOrDefault(e => e.cabSolOCTipoSolicitud == tipoSol && e.cabSolOCNoSolicitud == noSol);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.cabSolOCFinancieroBy = id;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }

        // PUT: api/CabSolOrdenCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabSolOrdenCompra(int id, CabSolOrdenCompra cabSolOrdenCompra)
        {
            if (id != cabSolOrdenCompra.cabSolOCID)
            {
                return BadRequest();
            }

            _context.Entry(cabSolOrdenCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabSolOrdenCompraExists(id))
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

        // POST: api/CabSolOrdenCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CabSolOrdenCompra>> PostCabSolOrdenCompra(CabSolOrdenCompra cabSolOrdenCompra)
        {
          if (_context.CabSolOrdenCompras == null)
          {
              return Problem("Entity set 'SolicitudContext.CabSolOrdenCompras'  is null.");
          }
            _context.CabSolOrdenCompras.Add(cabSolOrdenCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCabSolOrdenCompra), new { id = cabSolOrdenCompra.cabSolOCID }, cabSolOrdenCompra);
        }

        // DELETE: api/CabSolOrdenCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabSolOrdenCompra(int id)
        {
            if (_context.CabSolOrdenCompras == null)
            {
                return NotFound();
            }
            var cabSolOrdenCompra = await _context.CabSolOrdenCompras.FindAsync(id);
            if (cabSolOrdenCompra == null)
            {
                return NotFound();
            }

            _context.CabSolOrdenCompras.Remove(cabSolOrdenCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CabSolOrdenCompraExists(int id)
        {
            return (_context.CabSolOrdenCompras?.Any(e => e.cabSolOCID == id)).GetValueOrDefault();
        }
    }
}
