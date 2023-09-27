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

        private bool CabSolPagoExists(int id)
        {
            return (_context.CabSolPagos?.Any(e => e.CabPagoID == id)).GetValueOrDefault();
        }
    }
}
