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
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemSector>> GetItemSector(int id)
        {
          if (_context.ItemSectores == null)
          {
              return NotFound();
          }
            var itemSector = await _context.ItemSectores.FindAsync(id);

            if (itemSector == null)
            {
                return NotFound();
            }

            return itemSector;
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemSector(int id)
        {
            if (_context.ItemSectores == null)
            {
                return NotFound();
            }
            var itemSector = await _context.ItemSectores.FindAsync(id);
            if (itemSector == null)
            {
                return NotFound();
            }

            _context.ItemSectores.Remove(itemSector);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemSectorExists(int id)
        {
            return (_context.ItemSectores?.Any(e => e.ItmIdItem == id)).GetValueOrDefault();
        }
    }
}