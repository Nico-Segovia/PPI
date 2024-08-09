using Microsoft.AspNetCore.Mvc;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OrdenesInversionAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenInversionsController : ControllerBase
    {
        private readonly IOrdenInversionService _ordenInversionService;

        public OrdenInversionsController(IOrdenInversionService ordenInversionService)
        {
            _ordenInversionService = ordenInversionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenInversion>>> GetOrdenesInversion()
        {
            return await _ordenInversionService.GetOrdenesInversion();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenInversion>> GetOrdenInversion(int id)
        {
            return await _ordenInversionService.GetOrdenInversion(id);
        }

        [Authorize(Policy = "CanUpdateOrder")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenInversion(int id, OrdenInversion ordenInversion)
        {
            return await _ordenInversionService.PutOrdenInversion(id, ordenInversion);
        }

        [Authorize(Policy = "CanCreateOrder")]
        [HttpPost]
        public async Task<ActionResult<OrdenInversion>> PostOrdenInversion(OrdenInversion ordenInversion)
        {
            return await _ordenInversionService.PostOrdenInversion(ordenInversion);
        }

        [Authorize(Policy = "CanDeleteOrder")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenInversion(int id)
        {
            return await _ordenInversionService.DeleteOrdenInversion(id);
        }
    }
}
