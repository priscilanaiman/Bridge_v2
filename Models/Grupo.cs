namespace Bridge.Models;

public class Grupo
{
    public int IdGrupo { get; set; }
    public int IdUsuarioResponsable { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public DateTime FechaNacimiento { get; set; }
    public string? CondicionSalud { get; set; }
    public string Rol { get; set; } = "";

    public int Edad
    {
        get
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - FechaNacimiento.Year;
            if (hoy.Month < FechaNacimiento.Month || (hoy.Month == FechaNacimiento.Month && hoy.Day < FechaNacimiento.Day))
            {
                edad = edad - 1;
            }
            return edad;
        }
    }
}
