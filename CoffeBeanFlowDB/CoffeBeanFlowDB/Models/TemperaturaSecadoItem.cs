namespace CoffeBeanFlowDB.Models;

public class TemperaturaSecadoItem
{

    public int ID_Temperatura { get; set; }

    public int Lectura { get; set; }

    //Llave foránea
    public int ID_Secado { get; set; }


}
