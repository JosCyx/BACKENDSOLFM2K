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
        private readonly string _rutaService;
        private readonly string _rutaLocal;

        public DocumentoController(SolicitudContext context,IConfiguration config)
        {
            _context = context;
            //_rutaService = config.GetSection("Configuracion").GetSection("RutaService").Value;
            string _rutaLocal= "http:\\192.168.1.75\\Solicitudes\\Solicitud_Cotizacion";
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
        public  ActionResult UploadFiles( IFormFile archivos,string prefijo,int tipoSOl)
        {
        
            // Credenciales para servidor 
            var user = "";
            var password = "";
            var RedCredencial = new NetworkCredential(user, password);
            try
            {
                if (tipoSOl == 1)
                {
                    //coti
                    var rutaDOC = @"\\192.168.1.75\Solicitudes\Solicitud_Cotizacion\" + prefijo + archivos.FileName;
                    //using (FileStream newFile = System.IO.File.Create(Filepaths))//
                    using (var stream = System.IO.File.Create(rutaDOC))
                    {
                        archivos.CopyTo(stream);
                        stream.Flush();
                    }
                    var documento = new Documento();
                    documento.DocUrl = rutaDOC;

                    _context.Documentos.Add(documento);
                    _context.SaveChanges();

                }
                else if (tipoSOl == 2)
                {
                    var rutaDOC = @"\\192.168.1.75\Solicitudes\Solicitud_Orden_Compra\" + prefijo + archivos.FileName;
                    using (var stream = System.IO.File.Create(rutaDOC))
                    {
                        archivos.CopyTo(stream);
                        stream.Flush();
                    }
                    var documento = new Documento();
                    documento.DocUrl = rutaDOC;

                    _context.Documentos.Add(documento);
                    _context.SaveChanges();

                }
                else if (tipoSOl == 3)
                {
                    var rutaDOC = @"\\192.168.1.75\Solicitudes\Solicitud_Orden_Pago\" + prefijo + archivos.FileName;
                    using (var stream = System.IO.File.Create(rutaDOC))
                    {
                        archivos.CopyTo(stream);
                        stream.Flush();
                    }
                    var documento = new Documento();
                    documento.DocUrl = rutaDOC;

                    _context.Documentos.Add(documento);
                    _context.SaveChanges();
                }
                //Se guarda en la ruta del servidor 
                //using (var newFile = new FileStream(rutaDOC,File.Create,FileAccess.Write,FileShare.None))
                //{
                //    archivos.CopyTo(newFile);
                //}
                //return CreatedAtAction(nameof(PostDocumento), new { id = documento.DocId }, documento);
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
