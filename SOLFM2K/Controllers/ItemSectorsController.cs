using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSectorsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public ItemSectorsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/ItemSectors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemSector>>> GetItemSectores()
        {
          if (_context.ItemSectores == null)
          {
              return NotFound();
          }
            return await _context.ItemSectores.ToListAsync();
        }

        // GET: api/ItemSectors/5
        [HttpGet("{tipoSol}/{noSol}/{noDet}")]
        public async Task<ActionResult<IEnumerable<ItemSector>>> GetItemSector(int tipoSol, int noSol, int noDet)
        {
          if (_context.ItemSectores == null)
          {
              return NotFound();
          }
            //var itemSector = await _context.ItemSectores.FindAsync(id);
            var itemSector = await _context.ItemSectores.Where(item =>
                item.ItmTipoSol == tipoSol &&
                item.ItmNumSol == noSol &&
                item.ItmIdDetalle == noDet).ToListAsync();

            if(itemSector.Count == 0)
            {
                return Ok(0);
            }

            if (itemSector == null)
            {
                return NotFound();
            }


            return itemSector;
        }

        [HttpGet("GetLastItem")]
        public async Task<ActionResult<IEnumerable<ItemSector>>> GetLastItembySol(int idSol, int idDet)
        {
            // Llamada al procedimiento almacenado mediante Entity Framework Core
            var result = await _context.ItemSectores.FromSqlRaw("EXEC sp_GetLastItemSector @p0, @p1", idSol, idDet).ToListAsync();

            if (result.Count == 0)
            {
                return Ok(0);
            }
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }


        // PUT: api/ItemSectors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemSector(int id, ItemSector itemSector)
        {
            if (id != itemSector.ItmIdItem)
            {
                return BadRequest();
            }

            _context.Entry(itemSector).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemSectorExists(id))
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

        // POST: api/ItemSectors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemSector>> PostItemSector(ItemSector itemSector)
        {
          if (_context.ItemSectores == null)
          {
              return Problem("Entity set 'SolicitudContext.ItemSectores'  is null.");
          }
            _context.ItemSectores.Add(itemSector);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostItemSector), new { id = itemSector.ItmIdItem }, itemSector);
        }

        // DELETE: api/ItemSectors/5
        [HttpDelete("{tipoSol}/{noSol}/{noDet}/{noItm}")]
        public async Task<IActionResult> DeleteItemSector(int tipoSol, int noSol, int noDet, int noItm)
        {
            var itemSector = await _context.ItemSectores.FirstOrDefaultAsync(i =>
                i.ItmTipoSol == tipoSol &&
                i.ItmNumSol == noSol &&
                i.ItmIdDetalle == noDet&&
                i.ItmIdItem == noItm);

            if (itemSector == null)
            {
                return NotFound();
            }

            _context.ItemSectores.Remove(itemSector);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteAllItems")]
        public async Task<IActionResult> DeleteAllItembySol(int tipoSol, int noSol)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC deleteItemsbySol @tipoSol, @noSol",
                    new SqlParameter("@tipoSol", tipoSol),
                    new SqlParameter("@noSol", noSol));

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting items.");
            }
        }



        private bool ItemSectorExists(int id)
        {
            return (_context.ItemSectores?.Any(e => e.ItmIdItem == id)).GetValueOrDefault();
        }
    }
}
