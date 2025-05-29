namespace CoffeBeanFlowDB.Models;

public class SecadoItem
{
    // Llave primaria
    public int ID_Secado { get; set; }
    
    // Campos de la tabla
    public DateTime Finicio { get; set; } // Fecha inicio
    public decimal Dsecado { get; set; } // D secado
    public decimal Psolar { get; set; } // Porcentaje Solar
    public decimal Pmecanico { get; set; } // Porcentaje Mecanico

    public DateTime Ffinal { get; set; } // Fecha final
    
    // Llave foránea
    public string Nlote { get; set; } // Número de lote
}