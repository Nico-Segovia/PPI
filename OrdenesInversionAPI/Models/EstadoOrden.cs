using OrdenesInversionAPI.Models;

public class EstadoOrden
{
    public int Id { get; set; }
    public string DescripcionEstado { get; set; }

    public ICollection<OrdenInversion> OrdenesInversion { get; set; }
}
