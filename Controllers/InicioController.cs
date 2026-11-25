using Microsoft.AspNetCore.Mvc;
using Bridge.Models;

namespace Bridge.Controllers;

public class InicioController : Controller
{
    private BD _bd = new BD();

    public IActionResult Index()
    {
        string? idUsuario = HttpContext.Session.GetString("IdUsuario");
        string? nombre = HttpContext.Session.GetString("Nombre");
        if (idUsuario == null || nombre == null)
        {
            return RedirectToAction("Login", "Sesiones");
        }

        ViewBag.Nombre = nombre;
        ViewBag.Iniciales = nombre.Substring(0, 1).ToUpper();

        List<Grupo> grupos = _bd.ObtenerMisGrupos(int.Parse(idUsuario));
        if (grupos.Count == 0)
        {
            return View("SinGrupos");
        }

        ViewBag.Grupos = grupos;
        return View();
    }
}
