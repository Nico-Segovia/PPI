using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace OrdenesInversionAPI.Models
{
    public class OrdenesInversionContext : DbContext
    {
        public OrdenesInversionContext(DbContextOptions<OrdenesInversionContext> options)
    : base(options)
        { }

        // Constructor adicional para pruebas
        public OrdenesInversionContext(DbContextOptionsBuilder<OrdenesInversionContext> optionsBuilder)
            : base(optionsBuilder.Options)
        { }


        public DbSet<OrdenInversion> OrdenesInversion { get; set; }
        public virtual DbSet<ActivoFinanciero> ActivosFinancieros { get; set; }

        public DbSet<TipoActivo> TiposActivo { get; set; }
        public DbSet<EstadoOrden> EstadosOrden { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("OrdenesInversionDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdenInversion>()
                .Property(o => o.Precio)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrdenInversion>()
                .Property(o => o.MontoTotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<ActivoFinanciero>()
                .Property(a => a.PrecioUnitario)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
