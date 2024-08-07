using OrdenesInversionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public interface IEstadoOrdenService
    {
        Task<ActionResult<IEnumerable<EstadoOrden>>> GetEstadosOrden();
        Task<ActionResult<EstadoOrden>> GetEstadoOrden(int id);
        Task<IActionResult> PutEstadoOrden(int id, EstadoOrden estadoOrden);
        Task<ActionResult<EstadoOrden>> PostEstadoOrden(EstadoOrden estadoOrden);
        Task<IActionResult> DeleteEstadoOrden(int id);
    }
}
