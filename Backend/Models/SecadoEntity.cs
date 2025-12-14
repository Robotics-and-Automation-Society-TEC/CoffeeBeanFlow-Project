using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    [Table("secado")]
    public class SecadoEntity
    {
        // Clave primaria
        [Key]
        [Column("id_secado")]
        public int IdSecado { get; set; }

        // Foreign Key hacia Área_Acopio
        [Column("nlote")]
        [Required]
        [MaxLength(50)]
        public string Nlote { get; set; } = string.Empty;

        // Atributos simples
        [Column("finicio")]
        [Required]
        public DateTime Finicio { get; set; }

        [Column("dsecado")]
        public int? Dsecado { get; set; }

        [Column("ffinal")]
        public DateTime? Ffinal { get; set; }

        // Relación con AreaAcopio
        [ForeignKey("Nlote")]
        public virtual AreaAcopioEntity? AreaAcopio { get; set; }

        // Relaciones con entidades débiles
        public virtual ICollection<HumedadEntity> Humedades { get; set; } = new List<HumedadEntity>();
        public virtual ICollection<TermoHigrometriaEntity> TermoHigrometrias { get; set; } = new List<TermoHigrometriaEntity>();
        public virtual ICollection<TemperaturaSecadoEntity> TemperaturasSecado { get; set; } = new List<TemperaturaSecadoEntity>();
        public virtual ICollection<NcamaEntity> Ncamas { get; set; } = new List<NcamaEntity>();

        // Relación N:N con Bodega a través de Guardar_Cafe
        public virtual ICollection<GuardarCafeEntity> BodegasGuardadas { get; set; } = new List<GuardarCafeEntity>();
    }
}
