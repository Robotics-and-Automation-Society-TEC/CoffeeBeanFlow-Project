namespace CoffeBeanFlowDB.Models;

public class TermoHigrometriaItem
{
    // Llave primaria
    public int ID_Termo { get; set; }
    
    // Campos de la tabla
    public decimal Hrelativa { get; set; } // Humedad relativa
    public int Tinterna { get; set; } // Temperatura interna
    public int Texterna { get; set; } // Temperatura externa
    
    // Llave foránea
    public int ID_Secado { get; set; }
}