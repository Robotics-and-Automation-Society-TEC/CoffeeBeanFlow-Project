using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad débil: Registro de temperatura y humedad ambiental durante el secado
    /// </summary>
    [Table("termo_higrometria")]
    public class TermoHigrometriaEntity
    {
        [Key]
        [Column("id_termo_higrometria")]
        public int IdTermoHigrometria { get; set; }

        // Foreign Key hacia Secado (entidad fuerte)
        [Column("id_secado")]
        [Required]
        public int IdSecado { get; set; }

        [Column("fecha")]
        [Required]
        public DateTime Fecha { get; set; }

        [Column("temperatura_ambiente")]
        [Required]
        public decimal TemperaturaAmbiente { get; set; }

        [Column("humedad_ambiente")]
        [Required]
        public decimal HumedadAmbiente { get; set; }

        // Relación con entidad fuerte Secado
        [ForeignKey("IdSecado")]
        public virtual SecadoEntity? Secado { get; set; }
    }
}
