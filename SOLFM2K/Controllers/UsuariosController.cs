using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SOLFM2K.Models;
using SOLFM2K.Services;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SolicitudContext _context;
        private readonly ITokenService _tokenService; // Inyecta el servicio

        public UsuariosController(SolicitudContext context, ITokenService tokenService) // Inyecta el servicio
        {
            _context = context;
            _tokenService = tokenService; // Asigna el servicio
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

        [HttpPost("Login")]
        public async Task<ActionResult<object>> LoginUser([FromBody] LoginModel model)
        //public async Task<ActionResult<object>> LoginUser(string username, string pass)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(us => us.UsLogin == model.username);

            if (user == null)
            {
                return NotFound("El usuario no existe.");
            }

            if (user.UsContrasenia != model.pass)
            {
                return BadRequest("La contraseña no coincide.");
            }

            // Generar un token JWT utilizando el servicio
            //var token = _tokenService.GenerateToken(user);
            var token = _tokenService.GenerateToken(user, 720);

            // Crear un objeto que contenga el token y los datos del usuario
            var response = new
            {
                Token = token,
                Usuario = new
                {
                    usLogin = user.UsLogin,
                    usIdNomina = user.UsIdNomina
                }
            };

            // Devolver el objeto JSON en la respuesta
            return Ok(response); // Aquí usamos Ok() para indicar que la solicitud fue exitosa
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
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostUsuario), new { id = usuario.UsId }, usuario);
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
