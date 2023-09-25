using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SOLFM2K.Models;
using SOLFM2K.Services;
using SOLFM2K.Services.CryptoService;

namespace SOLFM2K.Controllers
{
    //[ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SolicitudContext _context;
        private readonly ICryptoService _cryptoService;

        public UsuariosController(SolicitudContext context, ICryptoService cryptoService) // Inyecta el servicio
        {
            _context = context;
            _cryptoService = cryptoService;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("BuscarUsuario")]
        public async Task<ActionResult<List<Usuario>>> BuscarUsuario(int tipoBusqueda, string terminoBusqueda)
        {
            if (tipoBusqueda < 1 || tipoBusqueda > 2)
            {
                return BadRequest("error tipo busqueda");
            }
            // Llama al procedimiento almacenado searchEmpleado
            var usuariosEncontrados = 
                await _context.Usuarios.FromSqlRaw("EXEC searchUsuario @p0, @p1", tipoBusqueda, terminoBusqueda).ToListAsync();

            if (usuariosEncontrados == null || usuariosEncontrados.Count == 0)
            {
                return NotFound("No se encontraron usuarios que coincidan con la búsqueda.");
            }

            // Devuelve la lista de usuarios encontrados
            return Ok(usuariosEncontrados);
        }



        // GET: api/Usuarios/5
        [HttpGet("{UsId}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int UsId)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(UsId);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{UsId}")]
        public async Task<IActionResult> PutUsuario(int UsId, Usuario usuario)
        {
            if (UsId != usuario.UsId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(UsId))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'SolicitudContext.Usuarios'  is null.");
            }

            if (usuario == null)
            {
                return BadRequest("El objeto 'usuario' no puede ser nulo.");
            }

            try
            {
                // Cifra la contraseña antes de guardarla en la base de datos
                string claveCifrada = _cryptoService.EncryptPassword(usuario.UsContrasenia);

                // Asigna la clave cifrada de vuelta a la propiedad 'UsContrasenia' de 'usuario'
                usuario.UsContrasenia = claveCifrada;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostUsuario), new { id = usuario.UsId }, usuario);
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                // Manejo de errores
                return BadRequest($"Error al guardar la configuración: {ex.Message}\nInner Exception: {innerException?.Message}");
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{UsId}")]
        public async Task<IActionResult> DeleteUsuario(int UsId)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(UsId);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int UsId)
        {
            return (_context.Usuarios?.Any(e => e.UsId == UsId)).GetValueOrDefault();
        }
    }
}
