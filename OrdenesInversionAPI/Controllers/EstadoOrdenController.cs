using Microsoft.AspNetCore.Mvc;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosOrdenController : ControllerBase
    {
        private readonly IEstadoOrdenService _estadoOrdenService;

        public EstadosOrdenController(IEstadoOrdenService estadoOrdenService)
        {
            _estadoOrdenService = estadoOrdenService;
        }

        // GET: api/EstadosOrden
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoOrden>>> GetEstadosOrden()
        {
            return await _estadoOrdenService.GetEstadosOrden();
        }

        // GET: api/EstadosOrden/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoOrden>> GetEstadoOrden(int id)
        {
            return await _estadoOrdenService.GetEstadoOrden(id);
        }

        // PUT: api/EstadosOrden/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoOrden(int id, EstadoOrden estadoOrden)
        {
            return await _estadoOrdenService.PutEstadoOrden(id, estadoOrden);
        }

        // POST: api/EstadosOrden
        [HttpPost]
        public async Task<ActionResult<EstadoOrden>> PostEstadoOrden(EstadoOrden estadoOrden)
        {
            return await _estadoOrdenService.PostEstadoOrden(estadoOrden);
        }

        // DELETE: api/EstadosOrden/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadoOrden(int id)
        {
            return await _estadoOrdenService.DeleteEstadoOrden(id);
        }
    }
}
