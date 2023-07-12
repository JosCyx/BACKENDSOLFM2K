﻿using System;
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
        [HttpGet("{CodDep}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int CodDep)
        {
          if (_context.Departamentos == null)
          {
              return NotFound();
          }
            var departamento = await _context.Departamentos.FindAsync(CodDep);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Departamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CodDep}")]
        public async Task<IActionResult> PutDepartamento(int CodDep, Departamento departamento)
        {
            if (CodDep != departamento.CodDep)
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
                if (!DepartamentoExists(CodDep))
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
                if (DepartamentoExists(departamento.CodDep))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartamento", new { CodDep = departamento.CodDep }, departamento);
        }

        // DELETE: api/Departamentoes/5
        [HttpDelete("{CodDep}")]
        public async Task<IActionResult> DeleteDepartamento(int CodDep)
        {
            if (_context.Departamentos == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.FindAsync(CodDep);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartamentoExists(int CodDep)
        {
            return (_context.Departamentos?.Any(e => e.CodDep == CodDep)).GetValueOrDefault();
        }
    }
}