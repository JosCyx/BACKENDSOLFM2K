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
    public class CabSolCotizacionsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public CabSolCotizacionsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/CabSolCotizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CabSolCotizacion>>> GetCabSolCotizacions()
        {
          if (_context.CabSolCotizacions == null)
          {
              return NotFound();
          }
            return await _context.CabSolCotizacions.ToListAsync();
        }

        // GET: api/CabSolCotizacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CabSolCotizacion>> GetCabSolCotizacion(int id)
        {
          if (_context.CabSolCotizacions == null)
          {
              return NotFound();
          }
            var cabSolCotizacion = await _context.CabSolCotizacions.FindAsync(id);

            if (cabSolCotizacion == null)
            {
                return NotFound();
            }

            return cabSolCotizacion;
        }

        // PUT: api/CabSolCotizacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabSolCotizacion(int id, CabSolCotizacion cabSolCotizacion)
        {
            if (id != cabSolCotizacion.CabSolCotNoSolicitud)
            {
                return BadRequest();
            }

            _context.Entry(cabSolCotizacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabSolCotizacionExists(id))
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

        // POST: api/CabSolCotizacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CabSolCotizacion>> PostCabSolCotizacion(CabSolCotizacion cabSolCotizacion)
        {
          if (_context.CabSolCotizacions == null)
          {
              return Problem("Entity set 'SolicitudContext.CabSolCotizacions'  is null.");
          }
            _context.CabSolCotizacions.Add(cabSolCotizacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCabSolCotizacion), new { id = cabSolCotizacion.CabSolCotNoSolicitud }, cabSolCotizacion);
        }

        // DELETE: api/CabSolCotizacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCabSolCotizacion(int id)
        {
            if (_context.CabSolCotizacions == null)
            {
                return NotFound();
            }
            var cabSolCotizacion = await _context.CabSolCotizacions.FindAsync(id);
            if (cabSolCotizacion == null)
            {
                return NotFound();
            }

            _context.CabSolCotizacions.Remove(cabSolCotizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CabSolCotizacionExists(int id)
        {
            return (_context.CabSolCotizacions?.Any(e => e.CabSolCotNoSolicitud == id)).GetValueOrDefault();
        }
    }
}