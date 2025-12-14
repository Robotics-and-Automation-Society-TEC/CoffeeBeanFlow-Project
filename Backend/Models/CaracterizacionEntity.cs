using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeBeanFlowAPI.Models;

namespace Backend.Models
{
    [Table("formulario_caracterizacion")]
    public class CaracterizacionEntity
    {
        [Key]
        [Column("tiempo")]
        public DateTime Tiempo { get; set; }

        [Required]
        [Column("nlote_area_acopio")]
        [MaxLength(50)]
        public string NloteAreaAcopio { get; set; } = string.Empty;

        [Column("dr_maduras")]
        public decimal? DRmaduras { get; set; }

        [Column("pc_debajo")]
        public decimal? PCdebajo { get; set; }

        [Column("proceso")]
        [MaxLength(100)]
        public string? Proceso { get; set; }

        [Column("l_asignado")]
        [MaxLength(50)]
        public string? LAsignado { get; set; }

        [Column("c_verdes")]
        public int? Cverdes { get; set; }

        [Column("c_objetivo")]
        public int? Cobjetivo { get; set; }

        [Column("c_inmaduras")]
        public int? Cinmaduras { get; set; }

        [Column("c_sobremaduras")]
        public int? Csobremaduras { get; set; }

        [Column("c_secas")]
        public int? Csecas { get; set; }

        [Column("m_tabla")]
        public decimal? Mtabla { get; set; }

        [Column("pc_verdes")]
        public decimal? PCverdes { get; set; }

        [Column("pc_secas")]
        public decimal? PCsecas { get; set; }

        [Column("pc_encima")]
        public decimal? PCencima { get; set; }

        [Column("e_maduracion")]
        public decimal? Emaduracion { get; set; }

        [Column("broca")]
        public decimal? Broca { get; set; }

        [Column("densidad")]
        public decimal? Densidad { get; set; }

        [Column("vanos")]
        public int? Vanos { get; set; }

        [Column("secos")]
        public int? Secos { get; set; }

        [Column("pc_objetivo")]
        public decimal? PCobjetivo { get; set; }

        // Relación con Área de Acopio
        [ForeignKey("NloteAreaAcopio")]
        public virtual AreaAcopioEntity? AreaAcopio { get; set; }

        // Relaciones 1:1 con entidades débiles
        public virtual RCsobremadurasEntity? RCsobremaduras { get; set; }
        public virtual RCinmadurasEntity? RCinmaduras { get; set; }
        public virtual RCmadurasEntity? RCmaduras { get; set; }
    }
}
