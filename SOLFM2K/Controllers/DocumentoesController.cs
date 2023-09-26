using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SOLFM2K.Services.CryptoService;
using static System.Net.WebRequestMethods;

namespace SOLFM2K.Controllers
{
    //[ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly SolicitudContext _context;
        private readonly ICryptoService _cryptoService;

        public DocumentoController(SolicitudContext context, ICryptoService cryptoService)
        {
            _context = context;
            _cryptoService = cryptoService;

        }

        // GET: api/Documentoes
        [HttpGet("GetDocumentos")]
        public async Task<ActionResult<IEnumerable<Documento>>> GetDocumentos(int tipoSol, int noSol)
        {
            var documentos = await _context.Documentos
              .Where(c => c.DocTipoSolicitud == tipoSol && c.DocNoSolicitud == noSol).ToListAsync();
            if (documentos == null)
            {
                return NotFound();
            }
            return documentos;
        }
        //Documentos 
        [HttpGet("visualizeFile")]
        public IActionResult visualizeFile(string fileName)
        {
            try
            {
                string rutaBase = @"\\192.168.1.75\Solicitudes\";
                string filePath = Path.Combine(rutaBase, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("El archivo no existe en el servidor ");
                }


                string username = "Sistemas";
                string password = ".Fundacion2k*";
                NetworkCredential networkCredential = new NetworkCredential(username, password);

                // Crear un HttpClient personalizado con las credenciales
                HttpClient httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = networkCredential.GetCredential(new Uri(rutaBase), "Basic")
                });

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);


                return File(fileBytes, "application/octet-stream", fileName);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en archivo: " + ex.Message);
            }

        }

        [HttpGet("viewFile")]
        public IActionResult viewFile(string fileName)
        {
            try
            {
                string rutaBase = @"\\192.168.1.75\Solicitudes\";
                string filePath = Path.Combine(rutaBase, fileName);
                
                //extrae las credenciales de la base de datos y desencripta la contraseña
                var credentialsDB = _context.ParamsConfs.FirstOrDefault(cr => cr.Identify == "SVSOLICITUDES");
                var svPass = _cryptoService.DecryptPassword(credentialsDB.Pass);

                //credenciales del servidor de archivos
                string username = credentialsDB.Content;
                string password = svPass;


                //configuracion de las credenciales para el objeto httpclient
                NetworkCredential networkCredential = new NetworkCredential(username, password);

                // Crear un HttpClient personalizado con las credenciales
                HttpClient httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = networkCredential.GetCredential(new Uri(rutaBase), "Basic")
                });

                // Realiza una solicitud HEAD para verificar la existencia del archivo
                HttpResponseMessage headResponse = httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, rutaBase + fileName)).Result;

                if (headResponse.IsSuccessStatusCode)
                {
                    // El archivo existe en el servidor, procede a descargarlo como arreglo de bytes
                    HttpResponseMessage getResponse = httpClient.GetAsync(rutaBase + fileName).Result;
                    if (getResponse.IsSuccessStatusCode)
                    {
                        byte[] fileBytes = getResponse.Content.ReadAsByteArrayAsync().Result;
                        return File(fileBytes, "application/octet-stream", fileName);
                    }
                    else
                    {
                        // Manejar el error de descarga del archivo
                        return StatusCode((int)getResponse.StatusCode, "Error al descargar el archivo: " + getResponse.ReasonPhrase);
                    }
                }
                else if (headResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    // El archivo no existe en el servidor.
                    return NotFound("El archivo no existe en el servidor.");
                }
                else
                {
                    // Manejar otros errores de la solicitud HEAD
                    return StatusCode((int)headResponse.StatusCode, "Error al verificar la existencia del archivo: " + headResponse.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Catch exterior, error en archivo: " + ex.Message);
            }
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
                documento.DocNombre = archivos.FileName;
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
