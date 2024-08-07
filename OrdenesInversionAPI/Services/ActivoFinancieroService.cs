using Microsoft.AspNetCore.Mvc;
using OrdenesInversionAPI.Models;
using OrdenesInversionAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivosFinancierosController : ControllerBase
    {
        private readonly IActivoFinancieroService _activoFinancieroService;

        public ActivosFinancierosController(IActivoFinancieroService activoFinancieroService)
        {
            _activoFinancieroService = activoFinancieroService;
        }

        // GET: api/ActivosFinancieros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivoFinanciero>>> GetActivosFinancieros()
        {
            return await _activoFinancieroService.GetActivosFinancieros();
        }

        // GET: api/ActivosFinancieros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivoFinanciero>> GetActivoFinanciero(int id)
        {
            return await _activoFinancieroService.GetActivoFinanciero(id);
        }

        // PUT: api/ActivosFinancieros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivoFinanciero(int id, ActivoFinanciero activoFinanciero)
        {
            return await _activoFinancieroService.PutActivoFinanciero(id, activoFinanciero);
        }

        // POST: api/ActivosFinancieros
        [HttpPost]
        public async Task<ActionResult<ActivoFinanciero>> PostActivoFinanciero(ActivoFinanciero activoFinanciero)
        {
            return await _activoFinancieroService.PostActivoFinanciero(activoFinanciero);
        }

        // DELETE: api/ActivosFinancieros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivoFinanciero(int id)
        {
            return await _activoFinancieroService.DeleteActivoFinanciero(id);
        }
    }
}
