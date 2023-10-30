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
    //[ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuariosController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public RolUsuariosController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/RolUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolUsuario>>> GetRolUsuarios()
        {
          if (_context.RolUsuarios == null)
          {
              return NotFound();
          }
            return await _context.RolUsuarios.ToListAsync();
        }
        //devuelve los roles que tiene disponibles el usuario consultado
        // GET: api/RolUsuarios/RuUsuario/{ruUsuario}
        [HttpGet("GetUsuariobyNom")]
        public async Task<ActionResult<IEnumerable<RolUsuario>>> GetRolUsuarioByRtRol(string usuario)
        {
            if (_context.RolUsuarios == null)
            {
                return NotFound();
            }

            var rolUsuarios = await _context.RolUsuarios.Where(r => r.RuLogin == usuario).ToListAsync();

            if (rolUsuarios == null || !rolUsuarios.Any())
            {
                return NotFound();
            }

            return rolUsuarios;
        }

        // GET: api/RolUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolUsuario>> GetRolUsuario(int id)
        {
          if (_context.RolUsuarios == null)
          {
              return NotFound();
          }
            var rolUsuario = await _context.RolUsuarios.FindAsync(id);

            if (rolUsuario == null)
            {
                return NotFound();
            }

            return rolUsuario;
        }

        // PUT: api/RolUsuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolUsuario(int id, RolUsuario rolUsuario)
        {
            if (id != rolUsuario.RuId)
            {
                return BadRequest();
            }

            _context.Entry(rolUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolUsuarioExists(id))
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

        // POST: api/RolUsuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RolUsuario>> PostRolUsuario(RolUsuario rolUsuario)
        {
            if (rolUsuario == null)
            {
                return BadRequest("La solicitud no contiene datos válidos");
            }

            // Verificar si ya existe un registro con la misma combinación de RtRol y RtTransaccion
            var existeRegistro = _context.RolUsuarios.Any(aut => aut.RuRol == rolUsuario.RuRol && aut.RuLogin == rolUsuario.RuLogin);
            if (existeRegistro)
            {
                return Conflict("El elemento ya existe");
            }
            else
            {
                try
                {
                    _context.RolUsuarios.Add(rolUsuario);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(PostRolUsuario), new { id = rolUsuario.RuId }, rolUsuario);
                }
                catch (DbUpdateException)
                {
                    // Manejar error al guardar en la base de datos
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al guardar en la base de datos");
                }
            }

        }

        // DELETE: api/RolUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolUsuario(int id)
        {
            if (_context.RolUsuarios == null)
            {
                return NotFound();
            }
            var rolUsuario = await _context.RolUsuarios.FindAsync(id);
            if (rolUsuario == null)
            {
                return NotFound();
            }

            _context.RolUsuarios.Remove(rolUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolUsuarioExists(int id)
        {
            return (_context.RolUsuarios?.Any(e => e.RuId == id)).GetValueOrDefault();
        }
    }
}
