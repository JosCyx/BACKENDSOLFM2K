using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using static System.Net.WebRequestMethods;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly SolicitudContext _context;
        

        public DocumentoController(SolicitudContext context)
        {
            _context = context;
       
        }

        // GET: api/Documentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documento>>> GetDocumentos()
        {
          if (_context.Documentos == null)
          {
              return NotFound();
          }
            return await _context.Documentos.ToListAsync();
        }

        // GET: api/Documentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> GetDocumento(int id)
        {
          if (_context.Documentos == null)
          {
              return NotFound();
          }
            var documento = await _context.Documentos.FindAsync(id);

            if (documento == null)
            {
                return NotFound();
            }

            return documento;
        }

        // PUT: api/Documentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumento(int id, Documento documento)
        {
            if (id != documento.DocId)
            {
                return BadRequest();
            }

            _context.Entry(documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentoExists(id))
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

        // POST: api/Documentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Documento>> PostDocumento(Documento documento)
        {
          if (_context.Documentos == null)
          {
              return Problem("Entity set 'SolicitudContext.Documentos'  is null.");
          }
            _context.Documentos.Add(documento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumento", new { id = documento.DocId }, documento);
        }
        // Subir archivos  PDF 
        [HttpPost]
        [Route("upload")]
        public ActionResult UploadFiles(IFormFile archivos, string prefijo, int tipoSol, int noSol)
        {
            // Credenciales para servidor 
            var user = "";
            var password = "";
            var redCredencial = new NetworkCredential(user, password);

            try
            {
                string rutaBase = @"\\192.168.1.75\Solicitudes\";
                string subdirectorio = "";

                switch (tipoSol)
                {
                    case 1:
                        subdirectorio = "Solicitud_Cotizacion";
                        break;
                    case 2:
                        subdirectorio = "Solicitud_Orden_Compra";
                        break;
                    case 3:
                        subdirectorio = "Solicitud_Orden_Pago";
                        break;
                    default:
                        return BadRequest("Tipo de solicitud no válido");
                }

                string rutaDOC = Path.Combine(rutaBase, subdirectorio, prefijo + archivos.FileName);

                using (var stream = System.IO.File.Create(rutaDOC))
                {
                    archivos.CopyTo(stream);
                    stream.Flush();
                }

                var documento = new Documento();
                documento.DocTipoSolicitud = tipoSol;
                documento.DocNoSolicitud = noSol;
                documento.DocUrl = rutaDOC;

                _context.Documentos.Add(documento);
                _context.SaveChanges();

                return Ok(archivos);
            }
            catch (Exception error)
            {
                return StatusCode(500, "Error al subir el archivo: " + error.Message);
            }
        }

        // DELETE: api/Documentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumento(int id)
        {
            if (_context.Documentos == null)
            {
                return NotFound();
            }
            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }

            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DocumentoExists(int id)
        {
            return (_context.Documentos?.Any(e => e.DocId == id)).GetValueOrDefault();
        }
    }
}
