using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class EstadosOrdenController : ControllerBase
{
    private readonly OrdenesInversionContext _context;

    public EstadosOrdenController(OrdenesInversionContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoOrden>>> GetEstadosOrden()
    {
        return await _context.EstadosOrdenes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoOrden>> GetEstadoOrden(int id)
    {
        var estadoOrden = await _context.EstadosOrdenes.FindAsync(id);

        if (estadoOrden == null)
        {
            return NotFound();
        }

        return estadoOrden;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEstadoOrden(int id, EstadoOrden estadoOrden)
    {
        if (id != estadoOrden.Id)
        {
            return BadRequest();
        }

        _context.Entry(estadoOrden).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EstadoOrdenExists(id))
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
    public async Task<ActionResult<EstadoOrden>> PostEstadoOrden(EstadoOrden estadoOrden)
    {
        _context.EstadosOrdenes.Add(estadoOrden);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEstadoOrden", new { id = estadoOrden.Id }, estadoOrden);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoOrden(int id)
    {
        var estadoOrden = await _context.EstadosOrdenes.FindAsync(id);
        if (estadoOrden == null)
        {
            return NotFound();
        }

        _context.EstadosOrdenes.Remove(estadoOrden);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EstadoOrdenExists(int id)
    {
        return _context.EstadosOrdenes.Any(e => e.Id == id);
    }
}
