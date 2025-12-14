using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models
{
    /// <summary>
    /// Tabla de relación N:N: Guardar_Cafe
    /// Conecta Secado con Bodega
    /// </summary>
    [Table("guardar_cafe")]
    public class GuardarCafeEntity
    {
        // Clave primaria compuesta (ID_Secado, ID_Bodega)
        [Column("id_secado")]
        public int IdSecado { get; set; }

        [Column("id_bodega")]
        public int IdBodega { get; set; }

        // Atributo de la relación
        [Column("cantidad_sacos")]
        public int CantidadSacos { get; set; }

        // Relaciones de navegación
        [ForeignKey("IdSecado")]
        public virtual SecadoEntity? Secado { get; set; }

        [ForeignKey("IdBodega")]
        public virtual BodegaEntity? Bodega { get; set; }
    }
}
