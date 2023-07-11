using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SOLFM2K.services;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly IuService _Iuservice;

        public RolsController(IuService iuService)
        {
            _Iuservice = iuService;
        }

        // GET: api/Rols
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> GetRols()
        {
            try
            {
                var rols = await _Iuservice.GetRolAsync();
                return Ok(rols);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<Rol>>> Put(Rol rol)
        {
            try
            {
                var rols = await _Iuservice.UpdateRolAsync(rol);
                return Ok(rols);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<Rol>>> Add(Rol rol)
        {
            try
            {
                var rols = await _Iuservice.AddRolAsync(rol);
                return Ok(rols);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //    public async Task<ActionResult<IEnumerable<Rol>>> GetRols()
        //    {
        //      if (_context.Rols == null)
        //      {
        //          return NotFound();
        //      }
        //        return await _context.Rols.ToListAsync();
        //    }

        //    // GET: api/Rols/5
        //    [HttpGet("{RoCodigo}")]
        //    public async Task<ActionResult<Rol>> GetRol(short RoCodigo)
        //    {
        //      if (_context.Rols == null)
        //      {
        //          return NotFound();
        //      }
        //        var rol = await _context.Rols.FindAsync(RoCodigo);

        //        if (rol == null)
        //        {
        //            return NotFound();
        //        }

        //        return rol;
        //    }

        //    // PUT: api/Rols/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{RoEmpresa}")]
        //    public async Task<IActionResult> PutRol(byte RoEmpresa, Rol rol)
        //    {
        //        if (RoEmpresa != rol.RoEmpresa)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(rol).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RolExists(RoEmpresa))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Rols
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Rol>> PostRol(Rol rol)
        //    {
        //      if (_context.Rols == null)
        //      {
        //          return Problem("Entity set 'SolicitudContext.Rols'  is null.");
        //      }
        //        _context.Rols.Add(rol);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetRol", new { id = rol.RoCodigo }, rol);
        //    }

        //    // DELETE: api/Rols/5
        //    [HttpDelete("{RoCodigo}")]
        //    public async Task<IActionResult> DeleteRol(short RoCodigo)
        //    {
        //        if (_context.Rols == null)
        //        {
        //            return NotFound();
        //        }
        //        var rol = await _context.Rols.FindAsync(RoCodigo);
        //        if (rol == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Rols.Remove(rol);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool RolExists(short RoCodigo)
        //    {
        //        return (_context.Rols?.Any(e => e.RoCodigo == RoCodigo)).GetValueOrDefault();
        //    }
        //}
    }
}
