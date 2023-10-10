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

        
        [HttpGet("GetDestinoFiles")]
        public async Task<ActionResult<IEnumerable<DestinoSolPago>>> GetDestinoFiles(int tipoSol, int noSol)
        {
            var destinoFiles = await _context.DestinoSolPagos.Where(c => c.DestPagTipoSol == tipoSol && c.DestPagNoSol == noSol).ToListAsync();
            if (destinoFiles == null)
            {
                return NotFound();
            }
            return destinoFiles;
        }


        /*[HttpGet("DownloadDestinoFile")]
        public async Task<IActionResult> DownloadDestinoFile()
        {
            var destinoFile = _context.DestinoSolPagos.FirstOrDefault(dt => dt.DestPagId == destinoFileId);

            //return Ok(destinoFile);

            if (destinoFile == null)
            {
                return NotFound();
            }

            // Supongamos que destinoFile.dest_pag_evidencia contiene la ruta del archivo en el servidor local
            //var filePath = destinoFile.DestPagEvidencia;
            var filePat = @"\\192.168.1.75\Solicitudes\Solicitud_Orden_Pago\Destino_Sol_Pago\SP PMC 3-0017\3-tipos-de-app-moviles.png";
            var filePath = Path.Combine(filePat);
            //return Ok(filePath);

            // Asegúrate de que el archivo exista en la ruta especificada
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("El archivo no existe en el directorio");
            }

            // Lee el contenido del archivo y devuelve como una respuesta
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }*/



        //Documentos  
        //MODIFICAR LA RUTA PARA QUE COINCIDA CON LA RUTA LOCAL DEL SERVIDOR
        [HttpGet("ViewFile")]
        public IActionResult ViewFile(string fileName)
        {
            try
            {
                // Ruta a la carpeta en la unidad C:
                string folderPath = @"\\192.168.1.75\Solicitudes\";

                // Combina la ruta de la carpeta con el nombre de archivo
                string filePath = Path.Combine(folderPath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("El archivo no existe en el directorio");
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
            //var user = "";
            //var password = "";
            //var redCredencial = new NetworkCredential(user, password);

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

        //suube archivos al servidor en la carpeta de solicitudes de pago y crea una carpeta por cada solicitud que se suba
        [HttpPost]
        [Route("UploadSolPagoDocs")]
        public ActionResult UploadSolPagoDocs(IFormFile archivos, string prefijo, string item)
        {
            try
            {
                string rutaBase = @"\\192.168.1.75\Solicitudes\Solicitud_Orden_Pago\Destino_Sol_Pago";

                // Crear una carpeta con el nombre del prefijo si no existe
                string carpetaNueva = Path.Combine(rutaBase, prefijo);
                if (!Directory.Exists(carpetaNueva))
                {
                    Directory.CreateDirectory(carpetaNueva);
                }

                string rutaDOC = Path.Combine(carpetaNueva, item + "-"+ archivos.FileName);

                using (var stream = System.IO.File.Create(rutaDOC))
                {
                    archivos.CopyTo(stream);
                    stream.Flush();
                }

                var response = new
                {
                    url = rutaDOC
                };

                return Ok(response);
            }
            catch (Exception error)
            {
                return StatusCode(500, "Error al subir el archivo: " + error.Message);
            }
        }
        //Se elimina desde servidor  y en la  base de datos
        [HttpDelete("DeleteFile")]
        public  IActionResult DeleteFile(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return BadRequest("La URL del archivo es nula o vacia");
                }
                if (System.IO.File.Exists(url))
                {
                    System.IO.File.Delete(url);
                    var documento = _context.Documentos.FirstOrDefault(d => d.DocUrl == url);
                    if (documento != null)
                    {
                        _context.Documentos.Remove(documento);
                        _context.SaveChanges();
                    }

                    return Ok(documento);
                }
                else
                {
                    return NotFound("El archivo no existe en el directorio");
                }
            }
            catch (Exception ex )
            {
                return StatusCode(500, "Error en archivo: " + ex.Message);
            }
        }
        /* [HttpPost]
         [Route("UploadSolPagoDocs")]
         public ActionResult UploadSolPagoDocs(IFormFile archivos, string prefijo)
         {

             try
             {
                 string rutaBase = @"\\192.168.1.75\Solicitudes\Solicitud_Orden_Pago\Destino_Sol_Pago";

                 string rutaDOC = Path.Combine(rutaBase, prefijo + archivos.FileName);

                 using (var stream = System.IO.File.Create(rutaDOC))
                 {
                     archivos.CopyTo(stream);
                     stream.Flush();
                 }

                 var response = new
                 {
                     url = rutaDOC
                 };

                 return Ok(response);
             }
             catch (Exception error)
             {
                 return StatusCode(500, "Error al subir el archivo: " + error.Message);
             }
         }*/



    }
}


