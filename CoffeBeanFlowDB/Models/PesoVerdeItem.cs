namespace CoffeBeanFlowDB.Models;

public class PesoVerdeItem
{
    // Llave primaria
    public int ID_PesoVerde { get; set; }
    
    // Campos de la tabla
    public decimal Winferiores { get; set; } // Peso inferiores
    public decimal Wfinal { get; set; } // Peso final
    public decimal WFinferior { get; set; } // Peso Final inferior
    
    // Llave foránea
    public int ID_PesoTrilla { get; set; }
}