using OrdenesInversionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public interface IActivoFinancieroService
    {
        Task<ActionResult<IEnumerable<ActivoFinanciero>>> GetActivosFinancieros();
        Task<ActionResult<ActivoFinanciero>> GetActivoFinanciero(int id);
        Task<IActionResult> PutActivoFinanciero(int id, ActivoFinanciero activoFinanciero);
        Task<ActionResult<ActivoFinanciero>> PostActivoFinanciero(ActivoFinanciero activoFinanciero);
        Task<IActionResult> DeleteActivoFinanciero(int id);
    }
}
