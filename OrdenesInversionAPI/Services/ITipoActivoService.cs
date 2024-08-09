using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public interface ITipoActivoService
    {
        Task<ActionResult<IEnumerable<string>>> GetTiposActivo(); 
    }
}
