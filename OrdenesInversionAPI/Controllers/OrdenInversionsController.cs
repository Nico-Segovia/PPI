using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class OrdenesInversionController : ControllerBase
{
    private readonly OrdenesInversionContext _context;

    public OrdenesInversionController(OrdenesInversionContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrdenInversion>>> GetOrdenesInversion()
    {
        return await _context.OrdenesInversiones
            .Include(o => o.Estado)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrdenInversion>> GetOrdenInversion(int id)
    {
        var ordenInversion = await _context.OrdenesInversiones
            .Include(o => o.Estado)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (ordenInversion == null)
        {
            return NotFound();
        }

        return ordenInversion;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrdenInversion(int id, OrdenInversion ordenInversion)
    {
        if (id != ordenInversion.Id)
        {
            return BadRequest();
        }

        _context.Entry(ordenInversion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrdenInversionExists(id))
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
    public async Task<ActionResult<OrdenInversion>> PostOrdenInversion(OrdenInversion ordenInversion)
    {
        _context.OrdenesInversiones.Add(ordenInversion);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetOrdenInversion", new { id = ordenInversion.Id }, ordenInversion);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrdenInversion(int id)
    {
        var ordenInversion = await _context.OrdenesInversiones.FindAsync(id);
        if (ordenInversion == null)
        {
            return NotFound();
        }

        _context.OrdenesInversiones.Remove(ordenInversion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrdenInversionExists(int id)
    {
        return _context.OrdenesInversiones.Any(e => e.Id == id);
    }
}
