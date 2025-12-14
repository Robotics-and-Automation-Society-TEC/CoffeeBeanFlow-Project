using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("rondas")]
    public class RondasEntity
    {
        [Key]
        [Column("id_rondas")]
        public int IdRondas { get; set; }

        [Required]
        [Column("id_catacion")]
        public int IdCatacion { get; set; }

        [Column("valor_calidad")]
        public decimal? ValorCalidad { get; set; }

        // Relación con Catación
        [ForeignKey("IdCatacion")]
        public virtual CatacionEntity? Catacion { get; set; }
    }
}
