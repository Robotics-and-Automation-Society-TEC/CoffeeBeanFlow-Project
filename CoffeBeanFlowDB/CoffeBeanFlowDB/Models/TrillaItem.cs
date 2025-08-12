namespace CoffeBeanFlowDB.Models;

public class TrillaItem
{
    // Llave primaria
    public int ID_Trilla { get; set; }
    
    // Campos de la tabla
    public decimal Psegundas { get; set; } // Porcentaje segundas
    public decimal Pmenudos { get; set; } // Porcentaje menudos
    public decimal Pinferiores { get; set; } // Porcentaje inferiores
    public decimal Pmadres { get; set; } // Porcentaje madres
    public decimal Pprimera { get; set; } // Porcentaje primera
    public decimal Pcaracolillo { get; set; } // Porcentaje caracolillo
    public decimal Pescogeduras { get; set; } // Peso escogeduras
    public decimal Pbarreduras { get; set; } // Porcentaje barreduras
    public decimal Pcataduras { get; set; } // Porcentaje cataduras
    public decimal POinferiores { get; set; } //Porcentaje otras inferiores
    public decimal RTeoricoSeleccion { get; set; } // Rendimiento Teórico Selección
    public decimal RTeoricoPelado { get; set; } // Rendimiento Teórico Pelado
    public decimal RfinalPelado { get; set; } // Rendimiento final Pelado
    public decimal RfinalSeleccion { get; set; } // Rendimiento final Selección
    public decimal WVerdeFinal { get; set; } // Peso Verde Final
    public decimal WVerdeTeorico { get; set; } // Peso Verde Teórico
    public DateTime FFinalReposo { get; set; } // Fecha Final Reposo
    public decimal HFinal { get; set; } // Humedad Final
    public decimal HInicial { get; set; } // Humedad Inicial
    
    // Llave foránea
    public string Nlote { get; set; } // Número de lote
}