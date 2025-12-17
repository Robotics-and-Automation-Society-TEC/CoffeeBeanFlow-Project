using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("catacion")]
    public class CatacionEntity
    {
        [Key]
        [Column("id_catacion")]
        public int IdCatacion { get; set; }

        [Column("nlote")]
        [StringLength(100)]
        public string? Nlote { get; set; }

        // Atributos simples
        [Column("limpio")]
        public decimal? Limpio { get; set; }

        [Column("defectuoso")]
        public decimal? Defectuoso { get; set; }

        [Column("ffreposo")]
        public DateTime? FFreposo { get; set; }

        [Column("overde")]
        public decimal? Overde { get; set; }

        [Column("quaker")]
        public decimal? Quaker { get; set; }

        [Column("ccverde")]
        public decimal? CCverde { get; set; }

        [Column("rtostado")]
        public decimal? Rtostado { get; set; }

        [Column("dfueste")]
        public decimal? Dfueste { get; set; }

        [Column("cccalidad")]
        public decimal? CCcalidad { get; set; }

        // Atributos C1 (Categoría 1)
        [Column("c1agrio")]
        public decimal? C1agrio { get; set; }

        [Column("c1hongos")]
        public decimal? C1hongos { get; set; }

        [Column("c1cerezaseca")]
        public decimal? C1cerezaseca { get; set; }

        [Column("c1negro")]
        public decimal? C1negro { get; set; }

        [Column("c1insectos")]
        public decimal? C1insectos { get; set; }

        [Column("c1me")]
        public decimal? C1ME { get; set; }

        // Atributos C2 (Categoría 2)
        [Column("c2flotador")]
        public decimal? C2flotador { get; set; }

        [Column("c2averanado")]
        public decimal? C2averanado { get; set; }

        [Column("c2pergamino")]
        public decimal? C2pergamino { get; set; }

        [Column("c2inmaduro")]
        public decimal? C2inmaduro { get; set; }

        [Column("c2concha")]
        public decimal? C2concha { get; set; }

        [Column("c2insectos")]
        public decimal? C2insectos { get; set; }

        // Atributo compuesto C2CP
        [Column("c2cascara")]
        public decimal? C2cascara { get; set; }

        [Column("c2pulpa")]
        public decimal? C2pulpa { get; set; }

        // Atributo compuesto C2PCM
        [Column("c2partido")]
        public decimal? C2partido { get; set; }

        [Column("c2cortado")]
        public decimal? C2cortado { get; set; }

        [Column("c2mordido")]
        public decimal? C2mordido { get; set; }

        [Column("c2negrop")]
        public decimal? C2negroP { get; set; }

        [Column("c2agriop")]
        public decimal? C2agrioP { get; set; }

        // Atributo compuesto Zaranda
        [Column("tres_sobre_dieciseis")]
        public decimal? TresSobreDieciseis { get; set; }

        [Column("veinte")]
        public decimal? Veinte { get; set; }

        [Column("diecinueve")]
        public decimal? Diecinueve { get; set; }

        [Column("dieciocho")]
        public decimal? Dieciocho { get; set; }

        [Column("diecisiete")]
        public decimal? Diecisiete { get; set; }

        [Column("dieciseis")]
        public decimal? Dieciseis { get; set; }

        [Column("quince")]
        public decimal? Quince { get; set; }

        [Column("catorce")]
        public decimal? Catorce { get; set; }

        [Column("trece")]
        public decimal? Trece { get; set; }

        // Atributo compuesto TonAgton
        [Column("tonagton_25")]
        public decimal? Tonagton25 { get; set; }

        [Column("tonagton_35")]
        public decimal? Tonagton35 { get; set; }

        [Column("tonagton_45")]
        public decimal? Tonagton45 { get; set; }

        [Column("tonagton_55")]
        public decimal? Tonagton55 { get; set; }

        [Column("tonagton_65")]
        public decimal? Tonagton65 { get; set; }

        [Column("tonagton_75")]
        public decimal? Tonagton75 { get; set; }

        [Column("tonagton_85")]
        public decimal? Tonagton85 { get; set; }

        [Column("tonagton_95")]
        public decimal? Tonagton95 { get; set; }

        // Atributo derivado
        [Column("pfinales")]
        public decimal? Pfinales { get; set; }

        // Relación 1:N con Rondas (atributo multivaluado)
        public virtual ICollection<RondasEntity> Rondas { get; set; } = new List<RondasEntity>();
    }
}
