namespace CoffeBeanFlowDB.Models;

public class Gbx_madurasItem
{
    // Llave primaria
    public int ID_Gbx_maduras { get; set; }
    
    // Campos de la tabla
    public decimal Valor { get; set; }
    
    // Llave foránea
    public int ID_maduras { get; set; }
}