namespace Bridge.Models;

public class Miembro
{
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Rol { get; set; } = "";

    public string Iniciales
    {
        get
        {
            return (Nombre.Substring(0, 1) + Apellido.Substring(0, 1)).ToUpper();
        }
    }
}
