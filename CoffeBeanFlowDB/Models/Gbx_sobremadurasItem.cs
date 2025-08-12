namespace CoffeBeanFlowDB.Models;

public class Gbx_sobremadurasItem
{
    // Llave primaria
    public int ID_Gbx_sobremaduras { get; set; }
    
    // Campos de la tabla
    public decimal Valor { get; set; }
    
    // Llave foránea
    public int ID_sobremaduras { get; set; }
}