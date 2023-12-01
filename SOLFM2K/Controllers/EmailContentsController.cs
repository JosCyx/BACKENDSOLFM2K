using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;
using SOLFM2K.Pruebas;

namespace SOLFM2K.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailContentsController : ControllerBase
    {
        private readonly SolicitudContext _context;

        public EmailContentsController(SolicitudContext context)
        {
            _context = context;
        }

        // GET: api/EmailContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailContent>>> GetEmailContents()
        {
          if (_context.EmailContents == null)
          {
              return NotFound();
          }
            return await _context.EmailContents.ToListAsync();
        }

        // GET: api/EmailContents/5
        [HttpGet("{id}")]
        public ActionResult<EmailContent> GetEmailContent(int id)
        {
            if (_context.EmailContents == null)
            {
                return NotFound();
            }
            var emailContent = _context.EmailContents.FirstOrDefault(e => e.EmailContTipoAccion == id);

            if (emailContent == null)
            {
                return NotFound();
            }

            return emailContent;
        }

        // PUT: api/EmailContents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmailContent(int id, EmailContent emailContent)
        {
            if (id != emailContent.EmailContId)
            {
                return BadRequest();
            }

            _context.Entry(emailContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailContentExists(id))
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

        // POST: api/EmailContents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmailContent>> PostEmailContent(EmailContent emailContent)
        {
          if (_context.EmailContents == null)
          {
              return Problem("Entity set 'SolicitudContext.EmailContents'  is null.");
          }
            _context.EmailContents.Add(emailContent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmailContent", new { id = emailContent.EmailContId }, emailContent);
        }

        // DELETE: api/EmailContents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmailContent(int id)
        {
            if (_context.EmailContents == null)
            {
                return NotFound();
            }
            var emailContent = await _context.EmailContents.FindAsync(id);
            if (emailContent == null)
            {
                return NotFound();
            }

            _context.EmailContents.Remove(emailContent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmailContentExists(int id)
        {
            return (_context.EmailContents?.Any(e => e.EmailContId == id)).GetValueOrDefault();
        }
    }
}
