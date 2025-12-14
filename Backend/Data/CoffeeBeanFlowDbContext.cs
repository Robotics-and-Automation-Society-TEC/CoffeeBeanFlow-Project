using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Models;
using Backend.Models;

namespace CoffeeBeanFlowAPI.Data
{
    public class CoffeeBeanFlowDbContext : DbContext
    {
        public CoffeeBeanFlowDbContext(DbContextOptions<CoffeeBeanFlowDbContext> options)
            : base(options)
        {
        }

        // DbSets para las entidades
        public DbSet<AreaAcopioEntity> AreaAcopio { get; set; }
        public DbSet<SecadoEntity> Secado { get; set; }
        public DbSet<HumedadEntity> Humedad { get; set; }
        public DbSet<TermoHigrometriaEntity> TermoHigrometria { get; set; }
        public DbSet<TemperaturaSecadoEntity> TemperaturaSecado { get; set; }
        public DbSet<NcamaEntity> Ncama { get; set; }
        public DbSet<BodegaEntity> Bodega { get; set; }
        public DbSet<GuardarCafeEntity> GuardarCafe { get; set; }
        public DbSet<TrillaEntity> Trilla { get; set; }
        public DbSet<PesoVerdeEntity> PesoVerde { get; set; }
        public DbSet<CaracterizacionEntity> Caracterizacion { get; set; }
        public DbSet<RCsobremadurasEntity> RCsobremaduras { get; set; }
        public DbSet<RCinmadurasEntity> RCinmaduras { get; set; }
        public DbSet<RCmadurasEntity> RCmaduras { get; set; }
        public DbSet<CatacionEntity> Catacion { get; set; }
        public DbSet<RondasEntity> Rondas { get; set; }
        public DbSet<EnviarMuestrasEntity> EnviarMuestras { get; set; }
        public DbSet<SuministraEntity> Suministra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Area_Acopio
            modelBuilder.Entity<AreaAcopioEntity>(entity =>
            {
                entity.HasKey(e => e.Nlote);
                entity.Property(e => e.Nlote).IsRequired();
                entity.Property(e => e.Nproductor).IsRequired();
                entity.Property(e => e.Nfinca).IsRequired();
            });

            // Configuración de Secado
            modelBuilder.Entity<SecadoEntity>(entity =>
            {
                entity.HasKey(e => e.IdSecado);
                
                // Relación con AreaAcopio (1:N)
                entity.HasOne(e => e.AreaAcopio)
                    .WithMany()
                    .HasForeignKey(e => e.Nlote)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de entidad débil: Humedad
            modelBuilder.Entity<HumedadEntity>(entity =>
            {
                entity.HasKey(e => e.IdHumedad);
                
                entity.HasOne(e => e.Secado)
                    .WithMany(s => s.Humedades)
                    .HasForeignKey(e => e.IdSecado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de entidad débil: TermoHigrometria
            modelBuilder.Entity<TermoHigrometriaEntity>(entity =>
            {
                entity.HasKey(e => e.IdTermoHigrometria);
                
                entity.HasOne(e => e.Secado)
                    .WithMany(s => s.TermoHigrometrias)
                    .HasForeignKey(e => e.IdSecado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de entidad débil: TemperaturaSecado
            modelBuilder.Entity<TemperaturaSecadoEntity>(entity =>
            {
                entity.HasKey(e => e.IdTemperaturaSecado);
                
                entity.HasOne(e => e.Secado)
                    .WithMany(s => s.TemperaturasSecado)
                    .HasForeignKey(e => e.IdSecado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de entidad débil: Ncama (atributo multivaluado)
            modelBuilder.Entity<NcamaEntity>(entity =>
            {
                entity.HasKey(e => e.IdNcama);
                
                entity.HasOne(e => e.Secado)
                    .WithMany(s => s.Ncamas)
                    .HasForeignKey(e => e.IdSecado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Bodega
            modelBuilder.Entity<BodegaEntity>(entity =>
            {
                entity.HasKey(e => e.IdBodega);
                
                // Relación con AreaAcopio (1:N)
                entity.HasOne(e => e.AreaAcopio)
                    .WithMany()
                    .HasForeignKey(e => e.Nlote)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de relación N:N Guardar_Cafe
            modelBuilder.Entity<GuardarCafeEntity>(entity =>
            {
                // Clave primaria compuesta
                entity.HasKey(e => new { e.IdSecado, e.IdBodega });
                
                // Relación con Secado
                entity.HasOne(e => e.Secado)
                    .WithMany(s => s.BodegasGuardadas)
                    .HasForeignKey(e => e.IdSecado)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Relación con Bodega
                entity.HasOne(e => e.Bodega)
                    .WithMany(b => b.SecadosGuardados)
                    .HasForeignKey(e => e.IdBodega)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Trilla
            modelBuilder.Entity<TrillaEntity>(entity =>
            {
                entity.HasKey(e => e.IdTrilla);
                
                // Relación con AreaAcopio (1:N)
                entity.HasOne(e => e.AreaAcopio)
                    .WithMany()
                    .HasForeignKey(e => e.Nlote)
                    .OnDelete(DeleteBehavior.Restrict);
                
                // Relación 1:1 con PesoVerde (Trilla tiene un PesoVerde)
                entity.HasOne(e => e.PesoVerde)
                    .WithOne(p => p.Trilla)
                    .HasForeignKey<PesoVerdeEntity>(p => p.IdPesoVerde)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de PesoVerde (entidad débil)
            modelBuilder.Entity<PesoVerdeEntity>(entity =>
            {
                entity.HasKey(e => e.IdPesoVerde);
            });

            // Configuración de Caracterizacion
            modelBuilder.Entity<CaracterizacionEntity>(entity =>
            {
                entity.HasKey(e => e.Tiempo);
                
                // Relación con AreaAcopio
                entity.HasOne(e => e.AreaAcopio)
                    .WithMany()
                    .HasForeignKey(e => e.NloteAreaAcopio)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de RCsobremaduras (entidad débil, relación 1:1)
            modelBuilder.Entity<RCsobremadurasEntity>(entity =>
            {
                entity.HasKey(e => e.IdSobremaduras);
                
                entity.HasOne(e => e.Caracterizacion)
                    .WithOne(c => c.RCsobremaduras)
                    .HasForeignKey<RCsobremadurasEntity>(e => e.Tiempo)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de RCinmaduras (entidad débil, relación 1:1)
            modelBuilder.Entity<RCinmadurasEntity>(entity =>
            {
                entity.HasKey(e => e.IdInmaduras);
                
                entity.HasOne(e => e.Caracterizacion)
                    .WithOne(c => c.RCinmaduras)
                    .HasForeignKey<RCinmadurasEntity>(e => e.Tiempo)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de RCmaduras (entidad débil, relación 1:1)
            modelBuilder.Entity<RCmadurasEntity>(entity =>
            {
                entity.HasKey(e => e.IdMaduras);
                
                entity.HasOne(e => e.Caracterizacion)
                    .WithOne(c => c.RCmaduras)
                    .HasForeignKey<RCmadurasEntity>(e => e.Tiempo)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Catacion
            modelBuilder.Entity<CatacionEntity>(entity =>
            {
                entity.HasKey(e => e.IdCatacion);
            });

            // Configuración de Rondas (atributo multivaluado de Catacion, relación 1:N)
            modelBuilder.Entity<RondasEntity>(entity =>
            {
                entity.HasKey(e => e.IdRondas);
                
                entity.HasOne(e => e.Catacion)
                    .WithMany(c => c.Rondas)
                    .HasForeignKey(e => e.IdCatacion)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de EnviarMuestras (relación N:N entre Trilla y Catacion)
            modelBuilder.Entity<EnviarMuestrasEntity>(entity =>
            {
                // Clave primaria compuesta
                entity.HasKey(e => new { e.IdTrilla, e.IdCatacion });
                
                // Relación con Trilla
                entity.HasOne(e => e.Trilla)
                    .WithMany()
                    .HasForeignKey(e => e.IdTrilla)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Relación con Catacion
                entity.HasOne(e => e.Catacion)
                    .WithMany()
                    .HasForeignKey(e => e.IdCatacion)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Suministra (relación N:N entre Trilla y Bodega)
            modelBuilder.Entity<SuministraEntity>(entity =>
            {
                // Clave primaria compuesta
                entity.HasKey(e => new { e.IdBodega, e.IdTrilla });
                
                // Relación con Bodega
                entity.HasOne(e => e.Bodega)
                    .WithMany()
                    .HasForeignKey(e => e.IdBodega)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Relación con Trilla
                entity.HasOne(e => e.Trilla)
                    .WithMany()
                    .HasForeignKey(e => e.IdTrilla)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
