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
    public class ProveedoresController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public ProveedoresController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/Proveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {
          if (_context.Proveedors == null)
          {
              return NotFound();
          }
            return await _context.Proveedors.ToListAsync();
        }

        [HttpGet("ProveedorbyRuc")]
        public async Task<ActionResult<IEnumerable<ProveedorTemplate>>> GetProveedorByRuc(string ruc)
        {
            var proveedor = await _context.ProveedorTemplates.FromSqlRaw("EXEC sp_buscarProveedorByRUC @p0", ruc).ToListAsync();
       

            if (proveedor == null || proveedor.Count == 0)
            {
                return NotFound();
            }

            return proveedor;
        }
        [HttpGet("ProveedorByNombre")]
        public async Task<ActionResult<IEnumerable<ProveedorTemplate>>> getProveedorByNombre(string nombre)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var ProveedorByNombre = await _context.ProveedorTemplates.FromSqlRaw("EXEC sp_buscarProveedorByNombre @p0", nombre).ToListAsync();

            if (ProveedorByNombre == null || ProveedorByNombre.Count == 0)
            {
                return NotFound();
            }

            return ProveedorByNombre;
        }

        // GET: api/Proveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
          if (_context.Proveedors == null)
          {
              return NotFound();
          }
            var proveedor = await _context.Proveedors.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // PUT: api/Proveedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.ProvId)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // POST: api/Proveedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
          if (_context.Proveedors == null)
          {
              return Problem("Entity set 'SolicitudContext.Proveedors'  is null.");
          }
            _context.Proveedors.Add(proveedor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProveedorExists(proveedor.ProvId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PostProveedor), new { id = proveedor.ProvId }, proveedor);
        }

        // DELETE: api/Proveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            if (_context.Proveedors == null)
            {
                return NotFound();
            }
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return (_context.Proveedors?.Any(e => e.ProvId == id)).GetValueOrDefault();
        }
    }
}
