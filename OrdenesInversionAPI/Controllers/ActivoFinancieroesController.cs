using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;


namespace OrdenesInversionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivoFinancieroesController : ControllerBase
    {
        private readonly OrdenesInversionContext _context;

        public ActivoFinancieroesController(OrdenesInversionContext context)
        {
            _context = context;
        }

        // GET: api/ActivoFinancieroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivoFinanciero>>> GetActivosFinancieros()
        {
            return await _context.ActivosFinancieros.ToListAsync();
        }

        // GET: api/ActivoFinancieroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivoFinanciero>> GetActivoFinanciero(int id)
        {
            var activoFinanciero = await _context.ActivosFinancieros.FindAsync(id);

            if (activoFinanciero == null)
            {
                return NotFound();
            }

            return activoFinanciero;
        }

        // PUT: api/ActivoFinancieroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivoFinanciero(int id, ActivoFinanciero activoFinanciero)
        {
            if (id != activoFinanciero.Id)
            {
                return BadRequest();
            }

            _context.Entry(activoFinanciero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivoFinancieroExists(id))
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

        // POST: api/ActivoFinancieroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivoFinanciero>> PostActivoFinanciero(ActivoFinanciero activoFinanciero)
        {
            _context.ActivosFinancieros.Add(activoFinanciero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivoFinanciero", new { id = activoFinanciero.Id }, activoFinanciero);
        }

        // DELETE: api/ActivoFinancieroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivoFinanciero(int id)
        {
            var activoFinanciero = await _context.ActivosFinancieros.FindAsync(id);
            if (activoFinanciero == null)
            {
                return NotFound();
            }

            _context.ActivosFinancieros.Remove(activoFinanciero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivoFinancieroExists(int id)
        {
            return _context.ActivosFinancieros.Any(e => e.Id == id);
        }
    }
}
