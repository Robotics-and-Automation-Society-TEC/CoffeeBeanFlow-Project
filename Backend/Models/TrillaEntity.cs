using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad fuerte: Trilla - Proceso de trillado del café
    /// </summary>
    [Table("trilla")]
    public class TrillaEntity
    {
        // Clave primaria
        [Key]
        [Column("id_trilla")]
        public int IdTrilla { get; set; }

        // Foreign Key hacia Área_Acopio
        [Column("nlote")]
        [Required]
        [MaxLength(50)]
        public string Nlote { get; set; } = string.Empty;

        // Atributos simples (18 atributos)
        [Column("hinicial")]
        public decimal? Hinicial { get; set; }

        [Column("hfinal")]
        public decimal? Hfinal { get; set; }

        [Column("rfinal_pelado")]
        public decimal? RFinalPelado { get; set; }

        [Column("rfinal_seleccion")]
        public decimal? RFinalSeleccion { get; set; }

        [Column("wverde_final")]
        public decimal? WverdeFinal { get; set; }

        [Column("rteorico_pelado")]
        public decimal? RteoricoPelado { get; set; }

        [Column("wverde_teorico")]
        public decimal? WverdeTeorico { get; set; }

        [Column("rteorico_seleccion")]
        public decimal? RTeoricoSeleccion { get; set; }

        [Column("ffinal_reposo")]
        public DateTime? FfinalReposo { get; set; }

        [Column("psegundas")]
        public decimal? Psegundas { get; set; }

        [Column("pcataduras")]
        public decimal? Pcataduras { get; set; }

        [Column("pbarreduras")]
        public decimal? Pbarreduras { get; set; }

        [Column("pescogeduras")]
        public decimal? Pescogeduras { get; set; }

        [Column("pcaracolillo")]
        public decimal? Pcaracolillo { get; set; }

        [Column("pprimera")]
        public decimal? Pprimera { get; set; }

        [Column("pmadres")]
        public decimal? Pmadres { get; set; }

        [Column("pmenudos")]
        public decimal? Pmenudos { get; set; }

        [Column("pinferiores")]
        public decimal? Pinferiores { get; set; }

        // Relación con AreaAcopio
        [ForeignKey("Nlote")]
        public virtual AreaAcopioEntity? AreaAcopio { get; set; }

        // Relación 1:1 con PesoVerde (entidad débil)
        public virtual PesoVerdeEntity? PesoVerde { get; set; }
    }
}
