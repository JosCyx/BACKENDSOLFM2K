﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

namespace SOLFM2K.Controllers
{
    //[ServiceFilter(typeof(JwtAuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoesController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public EmpleadoesController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Empleadoes
       [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
          if (_context.Empleados == null)
          {
              return NotFound();
          }
            return await _context.Empleados.ToListAsync();
        }

        // GET: api/Empleadoes/5
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpGet("{EmpleadoId}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int EmpleadoId)
        {
          if (_context.Empleados == null)
          {
              return NotFound();
          }
            var empleado = await _context.Empleados.FindAsync(EmpleadoId);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpGet("GetEmpleadobyNomina")]
        public async Task<ActionResult<IEnumerable<Empleado>>> getEmpleadobyNomina(string nomina)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var empleado = await _context.Empleados.Where(emp => emp.EmpleadoIdNomina == nomina).ToListAsync();

            if (empleado == null || empleado.Count == 0)
            {
                return NotFound();
            }

            return empleado;
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpGet("GetEmpleadobyArea")]
        public async Task<ActionResult<IEnumerable<Empleado>>> getEmpleadobyArea(int area)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var empleadoOP = await _context.Empleados.FromSqlRaw("EXEC sp_getEmpleadobyarea @p0", area).ToListAsync();

            if (empleadoOP == null || empleadoOP.Count == 0)
            {
                return NotFound();
            }

            return empleadoOP;
        }


        // PUT: api/Empleadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpPut("{EmpleadoId}")]
        public async Task<IActionResult> PutEmpleado(int EmpleadoId, Empleado empleado)
        {
            if (EmpleadoId != empleado.EmpleadoId)
            {
                return BadRequest();
            }

            _context.Entry(empleado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(EmpleadoId))
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
        [AllowAnonymous]
        [HttpPost("UpdateEmpleados")]
        public ActionResult UpdateEmpleados(string nombreTabla)
        {
            try
            {
                var parameters = new[]
                {
                new SqlParameter("@nombreTabla", nombreTabla)};

                _context.Database.ExecuteSqlRaw($"EXEC UpdateEmpleados @nombreTabla", parameters);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST: api/Empleadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
          if (_context.Empleados == null)
          {
              return Problem("Entity set 'SolicitudContext.Empleados'  is null.");
          }
            _context.Empleados.Add(empleado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmpleadoExists(empleado.EmpleadoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostEmpleado), new { id = empleado.EmpleadoId }, empleado);
        }

        // DELETE: api/Empleadoes/5
        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpDelete("{EmpleadoId}")]
        public async Task<IActionResult> DeleteEmpleado(int EmpleadoId)
        {
            if (_context.Empleados == null)
            {
                return NotFound();
            }
            var empleado = await _context.Empleados.FindAsync(EmpleadoId);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpleadoExists(int EmpleadoId)
        {
            return (_context.Empleados?.Any(e => e.EmpleadoId == EmpleadoId)).GetValueOrDefault();
        }
    }
}
