using Microsoft.AspNetCore.Mvc;
using OrdenesInversionAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public interface IOrdenInversionService
    {
        Task<ActionResult<IEnumerable<OrdenInversion>>> GetOrdenesInversion();
        Task<ActionResult<OrdenInversion>> GetOrdenInversion(int id);
        Task<IActionResult> PutOrdenInversion(int id, OrdenInversion ordenInversion);
        Task<ActionResult<OrdenInversion>> PostOrdenInversion(OrdenInversion ordenInversion);
        Task<IActionResult> DeleteOrdenInversion(int id);
    }
}
