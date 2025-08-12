using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Contexts
{
    public class Guardar_CafeContext : DbContext
    {
        public Guardar_CafeContext(DbContextOptions<Guardar_CafeContext> options) : base(options)
        {
        }

        public DbSet<Guardar_CafeItem> Guardar_Cafe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Guardar_CafeItem>()
                .HasKey(e => e.ID_Secado);

            // Configuración de precisión para campos decimales
            ConfigureDecimalPrecision(modelBuilder);
        }

        private void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        // Usar SetAnnotation en lugar de SetColumnType
                        property.SetAnnotation("Relational:ColumnType", "decimal(18,2)");
                    }
                }
            }
        }
    }
}