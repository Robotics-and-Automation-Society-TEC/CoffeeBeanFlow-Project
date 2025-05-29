namespace CoffeBeanFlowDB.Models;

public class Registro_FormularioItem
{
    // Llave primaria
    public int ID_Formulario { get; set; }
    
    // Llaves foráneas
    public int ID_sobremaduras { get; set; }
    public int ID_maduras { get; set; }
    public int ID_inmaduras { get; set; }
}