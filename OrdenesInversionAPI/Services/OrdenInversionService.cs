using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public class OrdenInversionService : IOrdenInversionService
    {
        private readonly OrdenesInversionContext _context;

        public OrdenInversionService(OrdenesInversionContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<OrdenInversion>>> GetOrdenesInversion()
        {
            return await _context.OrdenesInversion.ToListAsync();
        }

        public async Task<ActionResult<OrdenInversion>> GetOrdenInversion(int id)
        {
            var ordenInversion = await _context.OrdenesInversion.FindAsync(id);

            if (ordenInversion == null)
            {
                return new NotFoundResult();
            }

            return ordenInversion;
        }

        public async Task<IActionResult> PutOrdenInversion(int id, OrdenInversion ordenInversion)
        {
            if (id != ordenInversion.Id)
            {
                return new BadRequestResult();
            }

            // Obtener la orden existente de la base de datos
            var existingOrden = await _context.OrdenesInversion.FindAsync(id);

            if (existingOrden == null)
            {
                return new NotFoundResult();
            }

            // Verificar si se intenta actualizar otros campos además del estado
            if (existingOrden.IdCuentaInversion != ordenInversion.IdCuentaInversion ||
                existingOrden.NombreActivo != ordenInversion.NombreActivo ||
                existingOrden.Cantidad != ordenInversion.Cantidad ||
                existingOrden.Precio != ordenInversion.Precio ||
                existingOrden.Operacion != ordenInversion.Operacion ||
                existingOrden.MontoTotal != ordenInversion.MontoTotal)
            {
                return new BadRequestObjectResult("Solo se puede actualizar el estado de la orden.");
            }

            // Actualizar solo el estado de la orden
            existingOrden.Estado = ordenInversion.Estado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenInversionExists(id))
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

        public async Task<ActionResult<OrdenInversion>> PostOrdenInversion(OrdenInversion ordenInversion)
        {
            // Validación de la operación
            if (ordenInversion.Operacion != 'C' && ordenInversion.Operacion != 'V')
            {
                return new BadRequestObjectResult("La operación debe ser 'C' (Compra) o 'V' (Venta).");
            }

            // Obtener el activo financiero asociado a la orden
            var activo = await _context.ActivosFinancieros.FirstOrDefaultAsync(a => a.Nombre == ordenInversion.NombreActivo);

            if (activo == null)
            {
                return new BadRequestObjectResult("Activo financiero no encontrado.");
            }

            // Validación de cantidad y precio según el tipo de activo
            if (ordenInversion.Cantidad <= 0)
            {
                return new BadRequestObjectResult("La cantidad debe ser mayor que cero.");
            }

            if ((activo.TipoActivo == 2 || activo.TipoActivo == 3) && ordenInversion.Precio <= 0) // Bono o FCI
            {
                return new BadRequestObjectResult("El precio debe ser mayor que cero para Bonos y FCIs.");
            }

            // Calcular el MontoTotal según el tipo de activo
            switch (activo.TipoActivo)
            {
                case 1: // Acción
                    decimal precioAccion = activo.PrecioUnitario;
                    decimal comisiones = precioAccion * ordenInversion.Cantidad * 0.006m;
                    decimal impuestos = comisiones * 0.21m;
                    ordenInversion.MontoTotal = precioAccion * ordenInversion.Cantidad + comisiones + impuestos;
                    break;
                case 2: // Bono
                    comisiones = ordenInversion.Precio * ordenInversion.Cantidad * 0.002m;
                    impuestos = comisiones * 0.21m;
                    ordenInversion.MontoTotal = ordenInversion.Precio * ordenInversion.Cantidad + comisiones + impuestos;
                    break;
                case 3: // FCI
                    ordenInversion.MontoTotal = activo.PrecioUnitario * ordenInversion.Cantidad;
                    break;
                default:
                    return new BadRequestObjectResult("Tipo de activo no válido.");
            }

            // Asignar el estado inicial de la orden
            ordenInversion.Estado = 0; // "En proceso"

            try
            {
                _context.OrdenesInversion.Add(ordenInversion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return new StatusCodeResult(500); // Internal Server Error
            }

            return new CreatedAtActionResult("GetOrdenInversion", "OrdenInversions", new { id = ordenInversion.Id }, ordenInversion);
        }

        public async Task<IActionResult> DeleteOrdenInversion(int id)
        {
            var ordenInversion = await _context.OrdenesInversion.FindAsync(id);
            if (ordenInversion == null)
            {
                return new NotFoundResult();
            }

            _context.OrdenesInversion.Remove(ordenInversion);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool OrdenInversionExists(int id)
        {
            return _context.OrdenesInversion.Any(e => e.Id == id);
        }
    }
}


