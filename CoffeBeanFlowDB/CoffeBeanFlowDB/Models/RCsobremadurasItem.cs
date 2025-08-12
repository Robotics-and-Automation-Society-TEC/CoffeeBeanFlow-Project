namespace CoffeBeanFlowDB.Models;

public class RCsobremadurasItem
{
    // Llave primaria
    public int ID_sobremaduras { get; set; }
    
    // Campos de la tabla
    public decimal Promedio { get; set; }
    public string Observaciones { get; set; }
    public decimal Gbx { get; set; }
    
    // Llave foránea
    public DateTime Tiempo { get; set; }
}