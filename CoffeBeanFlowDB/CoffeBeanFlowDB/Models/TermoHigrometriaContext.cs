using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoffeBeanFlowDB.Models;

namespace CoffeBeanFlowDB.Contexts
{
    public class TermoHigrometriaContext : DbContext
    {
        public TermoHigrometriaContext(DbContextOptions<TermoHigrometriaContext> options) : base(options)
        {
        }

        public DbSet<TermoHigrometriaItem> TermoHigrometria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TermoHigrometriaItem>()
                .HasKey(e => e.ID_Termo);

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