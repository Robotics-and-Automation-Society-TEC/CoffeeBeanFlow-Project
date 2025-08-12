namespace CoffeBeanFlowDB.Models;

public class Guardar_CafeItem
{
    // Llave primaria
    public int ID_Secado { get; set; }
    
    // Llave foránea
    public int ID_Bodega { get; set; }
}