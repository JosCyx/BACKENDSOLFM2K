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
    public class FacturaSolPagoesController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public FacturaSolPagoesController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/FacturaSolPagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaSolPago>>> GetFacturaSolPagos()
        {
          if (_context.FacturaSolPagos == null)
          {
              return NotFound();
          }
            return await _context.FacturaSolPagos.ToListAsync();
        }

        // GET: api/FacturaSolPagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaSolPago>> GetFacturaSolPago(int id)
        {
          if (_context.FacturaSolPagos == null)
          {
              return NotFound();
          }
            var facturaSolPago = await _context.FacturaSolPagos.FindAsync(id);

            if (facturaSolPago == null)
            {
                return NotFound();
            }

            return facturaSolPago;
        }

        //obtener facturas segun los campos FactSpTipoSol y FactSpNoSol
        [HttpGet("FacturaSolPago/{tipoSol}/{noSol}")]
        public async Task<ActionResult<IEnumerable<FacturaSolPago>>> GetFacturaSolPago(int tipoSol, int noSol)
        {
          if (_context.FacturaSolPagos == null)
            {
              return NotFound();
          }
            var facturaSolPago = await _context.FacturaSolPagos.Where(x => x.FactSpTipoSol == tipoSol && x.FactSpNoSol == noSol).ToListAsync();

            if (facturaSolPago == null)
            {
                return NotFound();
            }

            return facturaSolPago;
        }

        // PUT: api/FacturaSolPagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateFactura")]
        public async Task<IActionResult> UpdateFactura(int noSol, int noFact, FacturaSolPago facturaSolPago)
        {
            var factura = await _context.FacturaSolPagos.AsNoTracking().Where(x => x.FactSpNoSol == noSol && x.FactSpNoFactura == noFact).ToListAsync();  
            if (factura == null)
            {
                return NotFound();
            }

            _context.Entry(facturaSolPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();

            
        }

        // POST: api/FacturaSolPagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaSolPago>> PostFacturaSolPago(FacturaSolPago facturaSolPago)
        {
          if (_context.FacturaSolPagos == null)
          {
              return Problem("Entity set 'SolicitudContext.FacturaSolPagos'  is null.");
          }
            _context.FacturaSolPagos.Add(facturaSolPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacturaSolPago", new { id = facturaSolPago.FactSpId }, facturaSolPago);
        }

        // DELETE: api/FacturaSolPagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturaSolPago(int id)
        {
            if (_context.FacturaSolPagos == null)
            {
                return NotFound();
            }
            var facturaSolPago = await _context.FacturaSolPagos.FindAsync(id);
            if (facturaSolPago == null)
            {
                return NotFound();
            }

            _context.FacturaSolPagos.Remove(facturaSolPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaSolPagoExists(int id)
        {
            return (_context.FacturaSolPagos?.Any(e => e.FactSpId == id)).GetValueOrDefault();
        }


        //CAMBIAR EL ESTADO DE LAS FACTURAS, 1 = ACTIVO, 0 = INACTIVO
        [HttpPut("ChangeEstadoFactura")]
        public IActionResult ChangeEstadoFactura(int noSol, int noFact, int estado)
        {
            var entityToUpdate = _context.FacturaSolPagos.FirstOrDefault(e => e.FactSpNoSol == noSol && e.FactSpNoFactura == noFact);

            if (entityToUpdate == null)
            {
                return NotFound(); // Devuelve un código 404 si el registro no existe.
            }

            // Actualiza el valor del campo deseado en el objeto entityToUpdate.
            entityToUpdate.FactSpEstado = estado;

            // Guarda los cambios en la base de datos.
            _context.SaveChanges();

            return NoContent(); // Devuelve un código 204 No Content para indicar éxito.
        }
        ////////////////////////////////////////////METODOS PARA EL MODELO DetalleFacturaPago////////////////////////////////////////////
        ///

        // GET: api/FacturaSolPagoes/5
        [HttpPost("DetalleFacturaPago")]
        public async Task<ActionResult<FacturaSolPago>> PostDetalleFacturaPago(DetalleFacturaPago detalleFacturaPago)
        {
          if (_context.DetalleFacturaPagos == null)
          {
              return Problem("Entity set 'SolicitudContext.DetalleFacturaPagos'  is null.");
          }
            _context.DetalleFacturaPagos.Add(detalleFacturaPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostDetalleFacturaPago), new { id = detalleFacturaPago.DetFactId }, detalleFacturaPago);
        }

        //obtener detalles de facturas segun el campo DetFactIdFactura
        [HttpGet("DetalleFacturaPago/{idFactura}")]
        public async Task<ActionResult<IEnumerable<DetalleFacturaPago>>> GetDetalleFacturaPago(int idFactura)
        {
          if (_context.DetalleFacturaPagos == null)
            {
              return NotFound();
          }
            var detalleFacturaPago = await _context.DetalleFacturaPagos.Where(x => x.DetFactIdFactura == idFactura).ToListAsync();

            if (detalleFacturaPago == null)
            {
                return NotFound();
            }

            return detalleFacturaPago;
        }

        [HttpPut("UpdateDetalleFactura")]
        public async Task<IActionResult> UpdateDetalleFactura(int idFacDet, int noDet, DetalleFacturaPago facturaSolPago)
        {
            var factura = await _context.DetalleFacturaPagos.AsNoTracking().Where(x => x.DetFactIdFactura == idFacDet && x.DetFactNoDetalle == noDet).ToListAsync();
           
            if (factura == null)
            {
                return NotFound();
            }

            _context.Entry(facturaSolPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();


        }

        ////////////////////////////////////////////METODOS PARA EL MODELO OCtemplateAX////////////////////////////////////////////

        // GET: api/FacturaSolPagoes/5
        [HttpPost("OCTemplateAX")]
        public async Task<ActionResult<OCAXTemplate>> PostOCTemplateAX(OCAXTemplate contentOC)
        {
            try
            {
                if (_context.OCAXTemplates == null)
                {
                    return Problem("Entity set 'SolicitudContext.OCAXTemplates' is null.");
                }

                var existingDetalle = await _context.OCAXTemplates
                    .FirstOrDefaultAsync(oc => oc.DetOrden == contentOC.DetOrden &&
                                                 oc.DetcodProducto == contentOC.DetcodProducto &&
                                                 oc.DetdesProducto == contentOC.DetdesProducto);

                if (existingDetalle != null)
                {
                    /*// Copiar propiedades excepto la clave primaria
                    _context.Entry(existingDetalle).CurrentValues.SetValues(contentOC);

                    // Guardar cambios
                    await _context.SaveChangesAsync();*/


                    // Eliminar el registro existente
                    _context.Remove(existingDetalle);
                    await _context.SaveChangesAsync();

                    // Agregar un nuevo registro con los cambios
                    _context.OCAXTemplates.Add(contentOC);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Si no existe, agregar un nuevo registro
                    _context.OCAXTemplates.Add(contentOC);
                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction(nameof(PostOCTemplateAX), new
                {
                    detOrden = contentOC.DetOrden,
                    detCodProducto = contentOC.DetcodProducto,
                    detDesProducto = contentOC.DetdesProducto
                }, contentOC);
            }
            catch (Exception ex)
            {
                return Problem($"Ocurrió un error al procesar la solicitud: {ex.Message}");
            }
        }





    }
}
