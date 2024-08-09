using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class ActivosFinancierosController : ControllerBase
{
    private readonly OrdenesInversionContext _context;

    public ActivosFinancierosController(OrdenesInversionContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivoFinanciero>>> GetActivosFinancieros()
    {
        return await _context.ActivosFinancieros.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivoFinanciero>> GetActivoFinanciero(int id)
    {
        var activoFinanciero = await _context.ActivosFinancieros.FindAsync(id);

        if (activoFinanciero == null)
        {
            return NotFound();
        }

        return activoFinanciero;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutActivoFinanciero(int id, ActivoFinanciero activoFinanciero)
    {
        if (id != activoFinanciero.Id)
        {
            return BadRequest();
        }

        _context.Entry(activoFinanciero).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActivoFinancieroExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ActivoFinanciero>> PostActivoFinanciero(ActivoFinanciero activoFinanciero)
    {
        _context.ActivosFinancieros.Add(activoFinanciero);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetActivoFinanciero", new { id = activoFinanciero.Id }, activoFinanciero);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivoFinanciero(int id)
    {
        var activoFinanciero = await _context.ActivosFinancieros.FindAsync(id);
        if (activoFinanciero == null)
        {
            return NotFound();
        }

        _context.ActivosFinancieros.Remove(activoFinanciero);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ActivoFinancieroExists(int id)
    {
        return _context.ActivosFinancieros.Any(e => e.Id == id);
    }
}
