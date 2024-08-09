namespace OrdenesInversionAPI.Models
{
    public class ActivoFinanciero
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Nombre { get; set; }
        public int TipoActivo { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
