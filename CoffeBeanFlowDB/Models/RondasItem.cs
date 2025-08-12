namespace CoffeBeanFlowDB.Models;

public class RondasItem
{
    // Llave primaria
    public int ID_Rondas { get; set; }
    
    // Campos de la tabla
    public decimal Valor_calidad { get; set; }
    
    // Llave foránea
    public int ID_catacion { get; set; }
    
}