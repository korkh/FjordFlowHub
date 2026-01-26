using FreightService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreightService.Data
{
    public class FreightDbContext : DbContext
    {
        public FreightDbContext(DbContextOptions<FreightDbContext> options) : base(options)
        {
        }

        public DbSet<Freight> Freights { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly set table names to lowercase for PostgreSQL compatibility
            modelBuilder.Entity<Freight>().ToTable("freights");
            modelBuilder.Entity<Cargo>().ToTable("cargos");

            // Define the one-to-one relationship explicitly
            modelBuilder.Entity<Freight>()
                .HasOne(f => f.Cargo)
                .WithOne(c => c.Freight)
                .HasForeignKey<Cargo>(c => c.FreightId);
        }
    }
}