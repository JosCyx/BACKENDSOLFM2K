    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using System.IO;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinoSolPagosController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public DestinoSolPagosController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/DestinoSolPagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestinoSolPago>>> GetDestinoSolPagos()
        {
          if (_context.DestinoSolPagos == null)
          {
              return NotFound();
          }
            return await _context.DestinoSolPagos.ToListAsync();
        }

        // GET: api/DestinoSolPagoes/GetEvidenciasBySolicitud
        [HttpGet("GetDestinoPagoBySolicitud")]
        public async Task<ActionResult<IEnumerable<DestinoSolPago>>> GetDestinoPagoBySolicitud(int tiposol, int nosol)
        {
            if (_context.DestinoSolPagos == null)
            {
                return NotFound();
            }

            var destinoSolPago = await _context.DestinoSolPagos.Where(d => d.DestPagTipoSol == tiposol && d.DestPagNoSol == nosol).ToListAsync();

            if (destinoSolPago == null || !destinoSolPago.Any())
            {
                return NotFound();
            }

            return destinoSolPago;
        }

        // GET: api/DestinoSolPagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestinoSolPago>> GetDestinoSolPago(int id)
        {
          if (_context.DestinoSolPagos == null)
          {
              return NotFound();
          }
            var destinoSolPago = await _context.DestinoSolPagos.FindAsync(id);

            if (destinoSolPago == null)
            {
                return NotFound();
            }

            return destinoSolPago;
        }

        // PUT: api/DestinoSolPagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestinoSolPago(int id, DestinoSolPago destinoSolPago)
        {
            if (id != destinoSolPago.DestPagId)
            {
                return BadRequest();
            }

            _context.Entry(destinoSolPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinoSolPagoExists(id))
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

        // POST: api/DestinoSolPagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DestinoSolPago>> PostDestinoSolPago(DestinoSolPago destinoSolPago)
        {
          if (_context.DestinoSolPagos == null)
          {
              return Problem("Entity set 'SolicitudContext.DestinoSolPagos'  is null.");
          }
            _context.DestinoSolPagos.Add(destinoSolPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestinoSolPago", new { id = destinoSolPago.DestPagId }, destinoSolPago);
        }

        // DELETE: api/DestinoSolPagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestinoSolPago(int id)
        {
            if (_context.DestinoSolPagos == null)
            {
                return NotFound();
            }
            var destinoSolPago = await _context.DestinoSolPagos.FindAsync(id);
            if (destinoSolPago == null)
            {
                return NotFound();
            }

            _context.DestinoSolPagos.Remove(destinoSolPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteDestinoSolPagoBySolicitud")]
        public async Task<IActionResult> DeleteDestinoSolPagoBySolicitud(int tiposol, int nosol)
        {
            if (_context.DestinoSolPagos == null)
            {
                return NotFound();
            }
            var destinoSolPago = await _context.DestinoSolPagos.Where(d => d.DestPagTipoSol == tiposol && d.DestPagNoSol == nosol).ToListAsync();
            if (destinoSolPago == null)
            {
                return NotFound();
            }

            _context.DestinoSolPagos.RemoveRange(destinoSolPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinoSolPagoExists(int id)
        {
            return (_context.DestinoSolPagos?.Any(e => e.DestPagId == id)).GetValueOrDefault();
        }

        [HttpGet("GetImage")]
        public IActionResult GetFile(string filePath)
        {
            try
            {
                // Verificar que la ruta sea válida (puedes agregar tus propias validaciones)
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                // Leer el archivo en bytes
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Devolver los bytes del archivo
                return File(fileBytes, "image/jpeg");

                // Devolver el archivo como base64 con el Content-Type adecuado
                //return Content(base64String, "image/jpeg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }
    }
}
