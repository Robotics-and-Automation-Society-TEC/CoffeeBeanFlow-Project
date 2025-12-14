using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("rc_maduras")]
    public class RCmadurasEntity
    {
        [Key]
        [Column("id_maduras")]
        public int IdMaduras { get; set; }

        [Required]
        [Column("tiempo")]
        public DateTime Tiempo { get; set; }

        [Column("gbx")]
        public decimal? Gbx { get; set; }

        [Column("promedio")]
        public decimal? Promedio { get; set; }

        [Column("observaciones")]
        [MaxLength(500)]
        public string? Observaciones { get; set; }

        // Relación 1:1 con Caracterización
        [ForeignKey("Tiempo")]
        public virtual CaracterizacionEntity? Caracterizacion { get; set; }
    }
}
