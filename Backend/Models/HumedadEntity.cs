using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad débil: Registro de humedad del café durante el proceso de secado
    /// </summary>
    [Table("humedad")]
    public class HumedadEntity
    {
        [Key]
        [Column("id_humedad")]
        public int IdHumedad { get; set; }

        // Foreign Key hacia Secado (entidad fuerte)
        [Column("id_secado")]
        [Required]
        public int IdSecado { get; set; }

        [Column("fecha")]
        [Required]
        public DateTime Fecha { get; set; }

        [Column("humedad")]
        [Required]
        public decimal Humedad { get; set; }

        // Relación con entidad fuerte Secado
        [ForeignKey("IdSecado")]
        public virtual SecadoEntity? Secado { get; set; }
    }
}
