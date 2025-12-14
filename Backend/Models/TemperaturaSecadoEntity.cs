using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad débil: Registro de temperatura del café durante el secado
    /// </summary>
    [Table("temperatura_secado")]
    public class TemperaturaSecadoEntity
    {
        [Key]
        [Column("id_temperatura_secado")]
        public int IdTemperaturaSecado { get; set; }

        // Foreign Key hacia Secado (entidad fuerte)
        [Column("id_secado")]
        [Required]
        public int IdSecado { get; set; }

        [Column("fecha")]
        [Required]
        public DateTime Fecha { get; set; }

        [Column("temperatura")]
        [Required]
        public decimal Temperatura { get; set; }

        // Relación con entidad fuerte Secado
        [ForeignKey("IdSecado")]
        public virtual SecadoEntity? Secado { get; set; }
    }
}
