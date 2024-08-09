using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public class ActivoFinancieroService : IActivoFinancieroService
    {
        private readonly OrdenesInversionContext _context;

        public ActivoFinancieroService(OrdenesInversionContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<ActivoFinanciero>>> GetActivosFinancieros()
        {
            return await _context.ActivosFinancieros.ToListAsync();
        }

        public async Task<ActionResult<ActivoFinanciero>> GetActivoFinanciero(int id)
        {
            var activoFinanciero = await _context.ActivosFinancieros.FindAsync(id);

            if (activoFinanciero == null)
            {
                return new NotFoundResult();
            }

            return activoFinanciero;
        }

        public async Task<ActivoFinanciero> GetActivoFinancieroPorNombre(string nombre)
        {
            return await _context.ActivosFinancieros.FirstOrDefaultAsync(a => a.Nombre == nombre);
        }

        public async Task<IActionResult> PutActivoFinanciero(int id, ActivoFinanciero activoFinanciero)
        {
            if (id != activoFinanciero.Id)
            {
                return new BadRequestResult();
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
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new NoContentResult();
        }

        public async Task<ActionResult<ActivoFinanciero>> PostActivoFinanciero(ActivoFinanciero activoFinanciero)
        {
            _context.ActivosFinancieros.Add(activoFinanciero);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetActivoFinanciero", "ActivosFinancieros", new { id = activoFinanciero.Id }, activoFinanciero);
        }

        public async Task<IActionResult> DeleteActivoFinanciero(int id)
        {
            var activoFinanciero = await _context.ActivosFinancieros.FindAsync(id);
            if (activoFinanciero == null)
            {
                return new NotFoundResult();
            }

            _context.ActivosFinancieros.Remove(activoFinanciero);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool ActivoFinancieroExists(int id)
        {
            return _context.ActivosFinancieros.Any(e => e.Id == id);
        }
    }
}
