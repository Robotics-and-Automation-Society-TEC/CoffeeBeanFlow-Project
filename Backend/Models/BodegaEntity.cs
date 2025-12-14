using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad fuerte: Bodega - Almacenamiento de café procesado
    /// </summary>
    [Table("bodega")]
    public class BodegaEntity
    {
        // Clave primaria
        [Key]
        [Column("id_bodega")]
        public int IdBodega { get; set; }

        // Foreign Key hacia Área_Acopio
        [Column("nlote")]
        [Required]
        [MaxLength(50)]
        public string Nlote { get; set; } = string.Empty;

        // Atributos simples (11 atributos)
        [Column("w_bellota")]
        public decimal? WBellota { get; set; }

        [Column("w_pergamino")]
        public decimal? WPergamino { get; set; }

        [Column("hfinal")]
        public decimal? Hfinal { get; set; }

        [Column("hinicial")]
        public decimal? Hinicial { get; set; }

        [Column("d_pergamino")]
        public decimal? DPergamino { get; set; }

        [Column("d_bellota")]
        public decimal? DBellota { get; set; }

        [Column("finicio_reposo")]
        public DateTime? FinicioReposo { get; set; }

        [Column("cantidad_sacos")]
        public int? CantidadSacos { get; set; }

        [Column("pmh_relativa")]
        public decimal? PMHRelativa { get; set; }

        [Column("pmt_interna")]
        public decimal? PMTInterna { get; set; }

        [Column("pmt_externa")]
        public decimal? PMTExterna { get; set; }

        // Relación con AreaAcopio
        [ForeignKey("Nlote")]
        public virtual AreaAcopioEntity? AreaAcopio { get; set; }

        // Relación N:N con Secado a través de Guardar_Cafe
        public virtual ICollection<GuardarCafeEntity> SecadosGuardados { get; set; } = new List<GuardarCafeEntity>();
    }
}
