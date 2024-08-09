using OrdenesInversionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public interface ITipoActivoService
    {
        Task<ActionResult<IEnumerable<TipoActivo>>> GetTiposActivo();
        Task<ActionResult<TipoActivo>> GetTipoActivo(int id);
        Task<IActionResult> PutTipoActivo(int id, TipoActivo tipoActivo);
        Task<ActionResult<TipoActivo>> PostTipoActivo(TipoActivo tipoActivo);
        Task<IActionResult> DeleteTipoActivo(int id);
    }
}
