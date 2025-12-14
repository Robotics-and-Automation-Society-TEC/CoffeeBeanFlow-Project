using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeBeanFlowAPI.Models;

namespace Backend.Models
{
    [Table("suministra")]
    public class SuministraEntity
    {
        [Required]
        [Column("id_bodega")]
        public int IdBodega { get; set; }

        [Required]
        [Column("id_trilla")]
        public int IdTrilla { get; set; }

        // Relaciones con las entidades
        [ForeignKey("IdBodega")]
        public virtual BodegaEntity? Bodega { get; set; }

        [ForeignKey("IdTrilla")]
        public virtual TrillaEntity? Trilla { get; set; }
    }
}
