using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeBeanFlowAPI.Models;

namespace Backend.Models
{
    [Table("enviar_muestras")]
    public class EnviarMuestrasEntity
    {
        [Required]
        [Column("id_trilla")]
        public int IdTrilla { get; set; }

        [Required]
        [Column("id_catacion")]
        public int IdCatacion { get; set; }

        [Column("ffinal_reposo")]
        public decimal? FfinalReposo { get; set; }

        // Relaciones con las entidades
        [ForeignKey("IdTrilla")]
        public virtual TrillaEntity? Trilla { get; set; }

        [ForeignKey("IdCatacion")]
        public virtual CatacionEntity? Catacion { get; set; }
    }
}
