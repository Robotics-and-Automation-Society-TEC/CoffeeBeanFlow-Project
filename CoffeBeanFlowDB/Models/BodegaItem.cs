namespace CoffeBeanFlowDB.Models;

public class BodegaItem
{
    // Llave primaria
    public int ID_Bodega { get; set; }
    
    // Campos de la tabla
    public decimal D_Bellota { get; set; } // Densidad Bellota
    public decimal D_Pergamino { get; set; } // Densidad Pergamino
    public decimal Hinicial { get; set; } // Humedad inicial
    public decimal Hfinal { get; set; } // Humedad final
    public decimal W_pergamino { get; set; } // Peso pergamino
    public decimal W_bellota { get; set; } // Peso bellota
    public DateTime FinicioReposo { get; set; } // Fecha inicio Reposo
    public int CantidadSacos { get; set; } // Cantidad de Sacos
    public decimal PMTexterna { get; set; } // Promedio Mensual Temperatura externa
    public decimal PMTinterna { get; set; } // Promedio Mensual Temperatura interna
    public decimal PMH_relativa { get; set; } // Promedio Mensual Humedad relativa
    
    // Llave foránea
    public string Nlote { get; set; } // Número de lote
}