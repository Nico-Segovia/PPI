using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public class EstadoOrdenService : IEstadoOrdenService
    {
        private readonly OrdenesInversionContext _context;

        public EstadoOrdenService(OrdenesInversionContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<EstadoOrden>>> GetEstadosOrden()
        {
            return await _context.EstadosOrden.ToListAsync();
        }

        public async Task<ActionResult<EstadoOrden>> GetEstadoOrden(int id)
        {
            var estadoOrden = await _context.EstadosOrden.FindAsync(id);

            if (estadoOrden == null)
            {
                return new NotFoundResult();
            }

            return estadoOrden;
        }

        public async Task<IActionResult> PutEstadoOrden(int id, EstadoOrden estadoOrden)
        {
            if (id != estadoOrden.Id)
            {
                return new BadRequestResult();
            }

            _context.Entry(estadoOrden).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoOrdenExists(id))
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

        public async Task<ActionResult<EstadoOrden>> PostEstadoOrden(EstadoOrden estadoOrden)
        {
            _context.EstadosOrden.Add(estadoOrden);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetEstadoOrden", "EstadosOrden", new { id = estadoOrden.Id }, estadoOrden);
        }

        public async Task<IActionResult> DeleteEstadoOrden(int id)
        {
            var estadoOrden = await _context.EstadosOrden.FindAsync(id);
            if (estadoOrden == null)
            {
                return new NotFoundResult();
            }

            _context.EstadosOrden.Remove(estadoOrden);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool EstadoOrdenExists(int id)
        {
            return _context.EstadosOrden.Any(e => e.Id == id);
        }
    }
}
