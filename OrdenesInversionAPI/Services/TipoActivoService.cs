using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public class TipoActivoService : ITipoActivoService
    {
        private readonly OrdenesInversionContext _context;

        public TipoActivoService(OrdenesInversionContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<TipoActivo>>> GetTiposActivo()
        {
            return await _context.TiposActivo.ToListAsync();
        }

        public async Task<ActionResult<TipoActivo>> GetTipoActivo(int id)
        {
            var tipoActivo = await _context.TiposActivo.FindAsync(id);

            if (tipoActivo == null)
            {
                return new NotFoundResult();
            }

            return tipoActivo;
        }

        public async Task<IActionResult> PutTipoActivo(int id, TipoActivo tipoActivo)
        {
            if (id != tipoActivo.Id)
            {
                return new BadRequestResult();
            }

            _context.Entry(tipoActivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoActivoExists(id))
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

        public async Task<ActionResult<TipoActivo>> PostTipoActivo(TipoActivo tipoActivo)
        {
            _context.TiposActivo.Add(tipoActivo);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetTipoActivo", "TiposActivo", new { id = tipoActivo.Id }, tipoActivo);
        }

        public async Task<IActionResult> DeleteTipoActivo(int id)
        {
            var tipoActivo = await _context.TiposActivo.FindAsync(id);
            if (tipoActivo == null)
            {
                return new NotFoundResult();
            }

            _context.TiposActivo.Remove(tipoActivo);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool TipoActivoExists(int id)
        {
            return _context.TiposActivo.Any(e => e.Id == id);
        }
    }
}
