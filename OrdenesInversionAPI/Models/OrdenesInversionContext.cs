using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;

public class OrdenesInversionContext : DbContext
{
    public DbSet<ActivoFinanciero> ActivosFinancieros { get; set; }
    public DbSet<EstadoOrden> EstadosOrdenes { get; set; }
    public DbSet<OrdenInversion> OrdenesInversiones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=OrdenesInversionDB;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de la relación entre EstadoOrden y OrdenInversion
        modelBuilder.Entity<EstadoOrden>()
            .HasMany(e => e.OrdenesInversion)
            .WithOne(o => o.Estado)
            .HasForeignKey(o => o.EstadoId);
    }
}
