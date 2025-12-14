using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Entidad débil: PesoVerde - Depende de Trilla
    /// Relación 1:1 identificadora con Trilla
    /// </summary>
    [Table("peso_verde")]
    public class PesoVerdeEntity
    {
        // Clave primaria (también es FK hacia Trilla - relación 1:1)
        [Key]
        [Column("id_peso_verde")]
        [ForeignKey("Trilla")]
        public int IdPesoVerde { get; set; }

        // Atributos simples (3 atributos)
        [Column("winferiores")]
        public decimal? Winferiores { get; set; }

        [Column("wfinal")]
        public decimal? Wfinal { get; set; }

        [Column("wfinal_inferiores")]
        public decimal? WFinalInferiores { get; set; }

        // Relación con Trilla (identificadora 1:1)
        public virtual TrillaEntity? Trilla { get; set; }
    }
}
