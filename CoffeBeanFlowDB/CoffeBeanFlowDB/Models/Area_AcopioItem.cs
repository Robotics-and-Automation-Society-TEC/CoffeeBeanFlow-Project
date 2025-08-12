namespace CoffeBeanFlowDB.Models;

public class Area_AcopioItem
{
    // Llave primaria
    public string Nlote { get; set; } // Número de lote

    // Campos de la tabla
    public decimal Rtotal { get; set; } // Rendimiento total
    public decimal Robjetivo { get; set; } // Rendimiento objetivo
    public decimal Rsobreobjetivo { get; set; } // Rendimiento objetivo

    public bool Vendido { get; set; }
    public decimal Disponible { get; set; }
    public string Enproceso { get; set; }
    public decimal Altura { get; set; }
    public string Zona { get; set; }
    public int Nrecibo { get; set; } // Número de recibo
    public string Nproductor { get; set; } // Nombre de productor
    public string Nfinca { get; set; } // Nombre de finca
    public string Despulpado { get; set; } //5 tipos
    public decimal Psegundas { get; set; } //Porcentaje segundas

    public decimal PDmecanicos { get; set; } //Porcentaje segundas
    public decimal PPulpaPergamino { get; set; } //Porcentaje segundas
    public decimal PPergaminoPulpa { get; set; } //Porcentaje segundas
    public decimal DFruta { get; set; } //Porcentaje segundas

    public decimal Dpergamino_humedo { get; set; } //Porcentaje segundas

    // Llave foránea
    public int ID_Secado { get; set; }
    
    
}