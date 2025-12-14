using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad débil: Registro de números de cama utilizados en el secado
    /// Representa el atributo multivaluado Ncama de Secado
    /// </summary>
    [Table("ncama")]
    public class NcamaEntity
    {
        [Key]
        [Column("id_ncama")]
        public int IdNcama { get; set; }

        // Foreign Key hacia Secado (entidad fuerte)
        [Column("id_secado")]
        [Required]
        public int IdSecado { get; set; }

        [Column("numero_cama")]
        [Required]
        [MaxLength(50)]
        public string NumeroCama { get; set; } = string.Empty;

        // Relación con entidad fuerte Secado
        [ForeignKey("IdSecado")]
        public virtual SecadoEntity? Secado { get; set; }
    }
}
