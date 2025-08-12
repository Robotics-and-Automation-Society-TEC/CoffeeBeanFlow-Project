namespace CoffeBeanFlowDB.Models;

public class Formulario_CaracterizacionItem
{
    // Llave primaria
    public DateTime Tiempo { get; set; }
    
    // Campos de la tabla
    public int Cinmaduras { get; set; } // Cerezas inmaduras
    public int Csobremaduras { get; set; } // Cerezas sobremaduras
    public int Csecas { get; set; } // Cerezas secas
    public int Cobjetivo { get; set; } // Cerezas objetivo
    public int Cverdes { get; set; } // Cerezas verdes
    public decimal PCdebajo { get; set; } // Porcentaje Cerezas debajo
    public string Proceso { get; set; }//Lavado,Miel,etc.
    public decimal DRmaduras { get; set; } // Determinación de Rango óptimo de maduras
    public decimal Mtabla { get; set; } // Muestreo tabla
    public decimal PCverdes { get; set; } // Porcentaje Cerezas verdes
    public decimal PCsecas { get; set; } // Porcentaje Cerezas secas
    public decimal PCencima { get; set; } // Porcentaje Cerezas encima
    public decimal Emaduracion { get; set; } // Escala maduración
    public decimal Broca { get; set; }
    public decimal Densidad { get; set; }
    public decimal Vanos { get; set; }
    public decimal PCobjetivo { get; set; } // Porcentaje Cerezas objetivo
    public decimal Secos { get; set; }
    
    // Llave foránea
    public string Nlote_AreaAcopio { get; set; } // Número de lote de Area_Acopio
}