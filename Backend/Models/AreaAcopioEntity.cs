using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    [Table("area_acopio")]
    public class AreaAcopioEntity
    {
        // ===== CLAVE PRIMARIA =====
        [Key]
        [Column("nlote")]
        [MaxLength(50)]
        public string Nlote { get; set; } = string.Empty;

        // ===== ATRIBUTOS SIMPLES =====
        [Column("altura")]
        public decimal Altura { get; set; }

        [Column("zona")]
        [MaxLength(100)]
        public string? Zona { get; set; }

        [Column("nrecibo")]
        public int Nrecibo { get; set; }

        [Column("nproductor")]
        [MaxLength(50)]
        public string? Nproductor { get; set; }

        [Column("nfinca")]
        [MaxLength(100)]
        public string? Nfinca { get; set; }

        [Column("robjetivo")]
        public decimal? Robjetivo { get; set; }

        [Column("rtotal")]
        public decimal? Rtotal { get; set; }

        [Column("vendido")]
        public bool Vendido { get; set; } = false;

        [Column("disponible")]
        public decimal? Disponible { get; set; }

        [Column("enproceso")]
        [MaxLength(50)]
        public string Enproceso { get; set; } = "No";

        // ===== ATRIBUTO COMPUESTO: Despulpado =====
        [Column("semilavado")]
        public bool? Semilavado { get; set; }

        [Column("natural")]
        public bool? Natural { get; set; }

        [Column("anaerobico")]
        public bool? Anaerobico { get; set; }

        [Column("otro")]
        [MaxLength(100)]
        public string? Otro { get; set; }

        [Column("miel")]
        public bool? Miel { get; set; }

        [Column("lavado")]
        public bool? Lavado { get; set; }

        // ===== ATRIBUTO COMPUESTO: Pruebas_Fisicas_BH =====
        [Column("pf_pulpa_pergamino")]
        public decimal? PF_Pulpa_Pergamino { get; set; }

        [Column("pf_dmecanicos")]
        public decimal? PF_DMecanicos { get; set; }

        [Column("pf_segundas")]
        public decimal? PF_Segundas { get; set; }

        [Column("pf_pergamino_pulpa")]
        public decimal? PF_Pergamino_Pulpa { get; set; }

        [Column("pdensidad_fruta")]
        public decimal? PDensidad_Fruta { get; set; }

        [Column("pdensidad_pergamino_humedo")]
        public decimal? PDensidad_Pergamino_Humedo { get; set; }
    }
}
