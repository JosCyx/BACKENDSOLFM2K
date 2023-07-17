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
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoesController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public DepartamentoesController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Departamentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
          if (_context.Departamentos == null)
          {
              return NotFound();
          }
            return await _context.Departamentos.ToListAsync();
        }

        // GET: api/Departamentoes/5
        [HttpGet("{DepId}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int DepId)
        {
          if (_context.Departamentos == null)
          {
              return NotFound();
          }
            var departamento = await _context.Departamentos.FindAsync(DepId);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Departamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{DepId}")]
        public async Task<IActionResult> PutDepartamento(int DepId, Departamento departamento)
        {
            if (DepId != departamento.DepId)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(DepId))
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

        // POST: api/Departamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
          if (_context.Departamentos == null)
          {
              return Problem("Entity set 'SolicitudContext.Departamentos'  is null.");
          }
            _context.Departamentos.Add(departamento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartamentoExists(departamento.DepId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostDepartamento), new { CodDep = departamento.DepId }, departamento);
        }

        // DELETE: api/Departamentoes/5
        [HttpDelete("{DepId}")]
        public async Task<IActionResult> DeleteDepartamento(int DepId)
        {
            if (_context.Departamentos == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.FindAsync(DepId);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartamentoExists(int DepId)
        {
            return (_context.Departamentos?.Any(e => e.DepId == DepId)).GetValueOrDefault();
        }
    }
}
