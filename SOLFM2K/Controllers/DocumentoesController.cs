using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SOLFM2K.Services.CryptoService;
using static System.Net.WebRequestMethods;
using System.Runtime.InteropServices;

namespace SOLFM2K.Controllers
{
    [ServiceFilter(typeof(JwtAuthorizationFilter))]
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
        //MODIFICAR LA RUTA PARA QUE COINCIDA CON LA RUTA LOCAL DEL SERVIDOR
        [HttpGet("ViewFile")]
        public IActionResult ViewFile(string fileName)
        {
            try
            {
                // Ruta a la carpeta en la unidad C:
                string folderPath = @"C:\prueba";

                // Combina la ruta de la carpeta con el nombre de archivo
                string filePath = Path.Combine(folderPath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("El archivo no existe en la carpeta de prueba");
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Devuelve el archivo como una respuesta HTTP
                return File(fileBytes, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en archivo: " + ex.Message);
            }
        }


        // Subir archivos  PDF 
        //MODIFICAR LA RUTA PARA QUE COINCIDA CON LA RUTA LOCAL DEL SERVIDOR
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

        private bool DocumentoExists(int id)
        {
            return (_context.Documentos?.Any(e => e.DocId == id)).GetValueOrDefault();
        }




        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        
    }
}


