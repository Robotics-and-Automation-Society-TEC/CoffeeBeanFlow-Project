namespace CoffeBeanFlowDB.Models;

public class HumedadItem
{
    // Llave primaria
    public int ID_Humedad { get; set; }
    
    // Campos de la tabla
    public decimal PHumedad { get; set; } // Porcentaje Humedad
    public int Temperatura { get; set; }
    
    // Llave foránea
    public int ID_Secado { get; set; }
}