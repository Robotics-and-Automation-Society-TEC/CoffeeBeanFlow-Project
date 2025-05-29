namespace CoffeBeanFlowDB.Models;

public class CatacionItem
{
    // Llave primaria
    public int ID_catacion { get; set; }
    
    // Información básica
    public DateTime FFreposo { get; set; } // Fecha Final de reposo
    public string Nlote { get; set; } // Número de lote
    
    // Estado del café
    public bool Defectuoso { get; set; }
    public bool Limpio { get; set; }
    
    // Características del café verde
    public string Overde { get; set; } // Olor verde
    public string CCcverde { get; set; } // Clasificación Color verde
    
    // Características del tostado
    public int Quaker { get; set; }
    public decimal Rtostado { get; set; } // Rendimiento/Resultado tostado
    public decimal Dtueste { get; set; } // Densidad en tueste
    
    // Clasificación y puntuación
    public int CCcalidad { get; set; } // Clasificación Calidad
    public decimal Pfinales { get; set; } // Puntos finales

    public int TAgtron { get; set ; }

    
    // Categoría 1 de defectos - Defectos primarios
    public decimal C1negro { get; set; }
    public decimal C1ME { get; set; } // Materia Extraña
    public decimal C1insectos { get; set; }
    public decimal C1cerezaseca { get; set; }
    public decimal C1hongos { get; set; }
    public decimal C1agrio { get; set; }
    
    // Categoría 2 de defectos - Defectos secundarios
    public int C2pergamino { get; set; }
    public int C2inmaduro { get; set; }
    public decimal C2negroP { get; set; } // Negro Parcial
    public decimal C2agrioP { get; set; } // Agrio Parcial/Pleno

    public decimal C2cascara_pulpa { get; set; }
    public decimal C2insectos { get; set; }
    public decimal C2averanado { get; set; }
    public decimal C2partido_cortado_mordido { get; set; }
    public decimal C2concha { get; set; }
    public decimal C2flotador { get; set; }

    
    // Medidas de zarandas/tamices (tamaños en mm)
    public decimal Trece { get; set; }
    public decimal Catorce { get; set; }
    public decimal Quince { get; set; }
    public decimal Dieciseis { get; set; }
    public decimal Diecisiete { get; set; }
    public decimal Dieciocho { get; set; }
    public decimal Diecinueve { get; set; }
    public decimal Veinte { get; set; }
    public decimal TresSobreDieciseis { get; set; } // 3/16
    
    public string Residuo { get; set; }

    
}