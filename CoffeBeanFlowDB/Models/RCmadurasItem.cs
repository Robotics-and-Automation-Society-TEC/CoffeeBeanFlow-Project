namespace CoffeBeanFlowDB.Models;

public class RCmadurasItem
{
    // Llave primaria
    public int ID_maduras { get; set; }
    
    // Campos de la tabla
    public decimal Promedio { get; set; }
    public string Observaciones { get; set; }
    public decimal Gbx { get; set; }
    
    // Llave foránea
    public DateTime Tiempo { get; set; }
}