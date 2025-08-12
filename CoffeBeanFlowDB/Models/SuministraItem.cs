namespace CoffeBeanFlowDB.Models;

public class SuministraItem
{
    // Llave primaria
    public int ID_Bodega { get; set; }
    
    // Llave foránea
    public int ID_Trilla { get; set; }
}