namespace CoffeBeanFlowDB.Models;

public class Gbx_inmadurasItem
{
    // Llave primaria
    public int ID_Gbx_inmaduras { get; set; }
    
    // Campos de la tabla
    public decimal Valor { get; set; }
    
    // Llave foránea
    public int ID_inmaduras { get; set; }
}