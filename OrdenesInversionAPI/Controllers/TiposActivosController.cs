using Microsoft.AspNetCore.Mvc;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposActivoController : ControllerBase
    {
        private readonly ITipoActivoService _tipoActivoService;

        public TiposActivoController(ITipoActivoService tipoActivoService)
        {
            _tipoActivoService = tipoActivoService;
        }

        // GET: api/TiposActivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoActivo>>> GetTiposActivo()
        {
            return await _tipoActivoService.GetTiposActivo();
        }

        // GET: api/TiposActivo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoActivo>> GetTipoActivo(int id)
        {
            return await _tipoActivoService.GetTipoActivo(id);
        }

        // PUT: api/TiposActivo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoActivo(int id, TipoActivo tipoActivo)
        {
            return await _tipoActivoService.PutTipoActivo(id, tipoActivo);
        }

        // POST: api/TiposActivo
        [HttpPost]
        public async Task<ActionResult<TipoActivo>> PostTipoActivo(TipoActivo tipoActivo)
        {
            return await _tipoActivoService.PostTipoActivo(tipoActivo);
        }

        // DELETE: api/TiposActivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoActivo(int id)
        {
            return await _tipoActivoService.DeleteTipoActivo(id);
        }
    }
}
