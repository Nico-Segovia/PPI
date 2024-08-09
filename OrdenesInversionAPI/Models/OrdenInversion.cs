using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrdenInversion
{
    public int Id { get; set; }

    [Required]
    public int IdCuentaInversion { get; set; }

    [Required]
    [StringLength(32)]
    public string NombreActivo { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Cantidad { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [Required]
    [RegularExpression("^[CV]$", ErrorMessage = "La operación debe ser 'C' (Compra) o 'V' (Venta).")]
    public char Operacion { get; set; }

    public int Estado { get; set; } // Valor por defecto es 0 ("En proceso")

    public decimal MontoTotal { get; set; }
}
