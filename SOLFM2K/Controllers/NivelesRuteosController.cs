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
    public class NivelesRuteosController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public NivelesRuteosController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/NivelesRuteos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NivelesRuteo>>> GetNivelesRuteos()
        {
          if (_context.NivelesRuteos == null)
          {
              return NotFound();
          }
            return await _context.NivelesRuteos.ToListAsync();
        }

        [HttpGet("GetNivelbyNivel")]
        public async Task<ActionResult<IEnumerable<NivelesRuteo>>> GetNivelbyNivel(int nivel)
        {
            var nivelConsultado = await _context.NivelesRuteos.Where(niv => niv.Nivel == nivel).ToListAsync();

            if (nivelConsultado == null)
            {
                return NotFound();
            }

            return nivelConsultado;
        }

        //retorna los niveles segun el estado indicado en el parametro
        [HttpGet("GetNivelByEstado")]
        public async Task<ActionResult<IEnumerable<NivelesRuteo>>> getNivelByEstado(char estadoRuteo)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var nivelesRuteo = await _context.NivelesRuteos.FromSqlRaw("EXEC sp_getNivelByEstado @p0", estadoRuteo).ToListAsync();

            if (nivelesRuteo == null || nivelesRuteo.Count == 0)
            {
                return NotFound();
            }

            return nivelesRuteo;
        }
        //METODOS PARA EXECUTAR PROCEDIMIENTOS ALMACENADOS
        //_context.Database.ExecuteSqlRawAsync ejecuta un sp en la base que no retorna valores, se usa en inserciones, actualizaciones, etc
        //_context.NivelesRuteos.FromSqlRaw, ejecuta un sp que si devuelve datos y los mapea en el modelo indicado antes del nombre del metodo

        // GET: api/NivelesRuteos/5
        [HttpGet("{CodRuteo}")]
        public async Task<ActionResult<NivelesRuteo>> GetNivelesRuteo(int CodRuteo)
        {
          if (_context.NivelesRuteos == null)
          {
              return NotFound();
          }
            var nivelesRuteo = await _context.NivelesRuteos.FindAsync(CodRuteo);

            if (nivelesRuteo == null)
            {
                return NotFound();
            }

            return nivelesRuteo;
        }

        // PUT: api/NivelesRuteos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CodRuteo}")]
        public async Task<IActionResult> PutNivelesRuteo(int CodRuteo, NivelesRuteo nivelesRuteo)
        {
            if (CodRuteo != nivelesRuteo.CodRuteo)
            {
                return BadRequest();
            }

            _context.Entry(nivelesRuteo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelesRuteoExists(CodRuteo))
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

        // POST: api/NivelesRuteos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NivelesRuteo>> PostNivelesRuteo(NivelesRuteo nivelesRuteo)
        {
          if (_context.NivelesRuteos == null)
          {
              return Problem("Entity set 'SolicitudContext.NivelesRuteos'  is null.");
          }
            _context.NivelesRuteos.Add(nivelesRuteo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostNivelesRuteo), new { CodRuteo = nivelesRuteo.CodRuteo }, nivelesRuteo);
        }

        // DELETE: api/NivelesRuteos/5
        //metodo para eliminar un elemento segun el Nivel
        [HttpDelete("{Nivel}")]
        public async Task<IActionResult> DeleteNivelesRuteo(int Nivel)
        {
            if (_context.NivelesRuteos == null)
            {
                return NotFound();
            }

            var nivelesRuteo = await _context.NivelesRuteos.FirstOrDefaultAsync(nr => nr.Nivel == Nivel);

            if (nivelesRuteo == null)
            {
                return NotFound();
            }

            _context.NivelesRuteos.Remove(nivelesRuteo);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        /*public async Task<IActionResult> DeleteNivelesRuteo(int CodRuteo)
        {
            if (_context.NivelesRuteos == null)
            {
                return NotFound();
            }
            var nivelesRuteo = await _context.NivelesRuteos.FindAsync(CodRuteo);
            if (nivelesRuteo == null)
            {
                return NotFound();
            }

            _context.NivelesRuteos.Remove(nivelesRuteo);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool NivelesRuteoExists(int CodRuteo)
        {
            return (_context.NivelesRuteos?.Any(e => e.CodRuteo == CodRuteo)).GetValueOrDefault();
        }
    }
}
