using Microsoft.AspNetCore.Mvc;
using Bridge.Models;

namespace Bridge.Controllers;

public class GruposController : Controller
{
    private BD _bd = new BD();

    public IActionResult Crear()
    {
        string? nombreUsuario = HttpContext.Session.GetString("Nombre");
        if (nombreUsuario == null)
        {
            return RedirectToAction("Login", "Sesiones");
        }

        CargarUsuarioEnLaVista(nombreUsuario);
        return View();
    }

    [HttpPost]
    public IActionResult Crear(string? nombre, string? apellido, DateTime? fechaNacimiento, string? rol, string? condicionSalud)
    {
        string? idUsuario = HttpContext.Session.GetString("IdUsuario");
        string? nombreUsuario = HttpContext.Session.GetString("Nombre");
        if (idUsuario == null || nombreUsuario == null)
        {
            return RedirectToAction("Login", "Sesiones");
        }

        string error;
        if (nombre == null)
        {
            error = "Falta el nombre.";
        }
        else if (apellido == null)
        {
            error = "Falta el apellido.";
        }
        else if (fechaNacimiento == null)
        {
            error = "Falta la fecha de nacimiento.";
        }
        else if (fechaNacimiento.Value >= DateTime.Today)
        {
            error = "La fecha de nacimiento tiene que ser anterior a hoy.";
        }
        else if (rol == null)
        {
            error = "Elegí qué sos de la persona.";
        }
        else
        {
            int idGrupo = _bd.CrearGrupo(int.Parse(idUsuario), nombre, apellido, fechaNacimiento.Value, condicionSalud, rol);
            return RedirectToAction("Ver", new { id = idGrupo });
        }

        CargarUsuarioEnLaVista(nombreUsuario);
        ViewBag.Error = error;
        ViewBag.NombreAdultoMayor = nombre;
        ViewBag.Apellido = apellido;
        ViewBag.CondicionSalud = condicionSalud;
        if (fechaNacimiento != null)
        {
            ViewBag.FechaNacimiento = fechaNacimiento.Value.ToString("yyyy-MM-dd");
        }
        return View();
    }

    public IActionResult Ver(int id)
    {
        string? idUsuario = HttpContext.Session.GetString("IdUsuario");
        string? nombreUsuario = HttpContext.Session.GetString("Nombre");
        if (idUsuario == null || nombreUsuario == null)
        {
            return RedirectToAction("Login", "Sesiones");
        }

        Grupo? grupo = _bd.ObtenerGrupo(id, int.Parse(idUsuario));
        if (grupo == null)
        {
            return RedirectToAction("Index", "Inicio");
        }

        HttpContext.Session.SetString("IdGrupo", grupo.IdGrupo.ToString());
        CargarUsuarioEnLaVista(nombreUsuario);
        ViewBag.Grupo = grupo;
        ViewBag.Miembros = _bd.ObtenerMiembrosDelGrupo(grupo.IdGrupo);
        ViewBag.SeccionInicio = "page";
        if (grupo.CondicionSalud == null)
        {
            ViewBag.CondicionSalud = "Sin condición de salud cargada";
        }
        else
        {
            ViewBag.CondicionSalud = grupo.CondicionSalud;
        }
        return View();
    }

    private void CargarUsuarioEnLaVista(string nombreUsuario)
    {
        ViewBag.Nombre = nombreUsuario;
        ViewBag.Iniciales = nombreUsuario.Substring(0, 1).ToUpper();
    }
}
