using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SOLFM2K.Services.CryptoService;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamsConfsController : ControllerBase
    {
        private readonly SolicitudContext _context;
        private readonly ICryptoService _cryptoService;

        public ParamsConfsController(SolicitudContext context, ICryptoService cryptoService)
        {
            _context = context;
            _cryptoService = cryptoService;
        }

        // GET: api/ParamsConfs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParamsConf>>> GetParamsConfs()
        {
          if (_context.ParamsConfs == null)
          {
              return NotFound();
          }
            return await _context.ParamsConfs.ToListAsync();
        }

        // GET: api/ParamsConfs
        [HttpGet("GetPrambyIdentify")]
        public async Task<ActionResult<IEnumerable<ParamsConf>>> GetPrambyIdentify(string identify)
        {
            //var parametro = _context.ParamsConfs.FirstOrDefault(pr => pr.Identify == identify);
            var param = await _context.ParamsConfs.Where(pr => pr.Identify == identify).ToListAsync();
            if (param == null)
            {
                return NotFound();
            }

            if (param.Count == 0)
            {
                return NoContent();
            }

            return param;
        }

        // GET: api/ParamsConfs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParamsConf>> GetParamsConf(int id)
        {
          if (_context.ParamsConfs == null)
          {
              return NotFound();
          }
            var paramsConf = await _context.ParamsConfs.FindAsync(id);

            if (paramsConf == null)
            {
                return NotFound();
            }

            return paramsConf;
        }

        // PUT: api/ParamsConfs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParamsConf(int id, ParamsConf paramsConf)
        {
            if (id != paramsConf.Id)
            {
                return BadRequest();
            }

            _context.Entry(paramsConf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParamsConfExists(id))
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

        // POST: api/ParamsConfs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParamsConf>> PostParamsConf(ParamsConf paramsConf)
        {
            if (paramsConf == null)
            {
                return BadRequest("El objeto 'paramsConf' no puede ser nulo.");
            }

            try
            {
                // Cifra la contraseña antes de guardarla en la base de datos
                string claveCifrada = _cryptoService.EncryptPassword(paramsConf.Pass);

                // Asigna la clave cifrada de vuelta a la propiedad 'Pass' de 'paramsConf'
                paramsConf.Pass = claveCifrada;

                _context.ParamsConfs.Add(paramsConf);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostParamsConf), new { id = paramsConf.Id }, paramsConf);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return BadRequest($"Error al guardar la configuración: {ex.Message}");
            } 
        }

        // DELETE: api/ParamsConfs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParamsConf(int id)
        {
            if (_context.ParamsConfs == null)
            {
                return NotFound();
            }
            var paramsConf = await _context.ParamsConfs.FindAsync(id);
            if (paramsConf == null)
            {
                return NotFound();
            }

            _context.ParamsConfs.Remove(paramsConf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParamsConfExists(int id)
        {
            return (_context.ParamsConfs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
