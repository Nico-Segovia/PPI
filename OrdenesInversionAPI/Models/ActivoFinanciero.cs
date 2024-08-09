using System.ComponentModel.DataAnnotations;

public class ActivoFinanciero
{
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Ticker { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    public TipoActivoEnum TipoActivo { get; set; }

    public decimal? PrecioUnitario { get; set; }
}

public enum TipoActivoEnum
{
    Accion = 1,
    Bono = 2,
    FCI = 3
}
