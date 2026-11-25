using Microsoft.AspNetCore.Mvc;

namespace Bridge.Controllers;

public class InicioController : Controller
{
    public IActionResult Index()
    {
        string? nombre = HttpContext.Session.GetString("Nombre");
        if (nombre == null)
        {
            return RedirectToAction("Login", "Sesiones");
        }

        ViewBag.Nombre = nombre;
        ViewBag.Iniciales = nombre.Substring(0, 1).ToUpper();
        return View();
    }
}
