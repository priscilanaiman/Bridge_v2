using Microsoft.AspNetCore.Mvc;
using Bridge.Models;

namespace Bridge.Controllers;

public class SesionesController : Controller
{
    private BD _bd = new BD();

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string? email, string? contrasena)
    {
        Usuario? usuario = null;
        if (email != null && contrasena != null)
        {
            usuario = _bd.IniciarSesion(email, contrasena);
        }

        if (usuario == null)
        {
            ViewBag.Error = "El email o la contraseña no son correctos.";
            ViewBag.Email = email;
            return View();
        }

        GuardarUsuarioEnSesion(usuario);
        return RedirectToAction("Index", "Inicio");
    }

    public IActionResult Registrarse()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrarse(string? nombre, string? apellido, string? email, string? contrasena, bool aceptaTerminos)
    {
        string error;
        if (nombre == null)
        {
            error = "Falta tu nombre.";
        }
        else if (apellido == null)
        {
            error = "Falta tu apellido.";
        }
        else if (email == null || email.IndexOf("@") < 1)
        {
            error = "Revisá el email.";
        }
        else if (contrasena == null || contrasena.Length < 8)
        {
            error = "La contraseña tiene que tener al menos 8 caracteres.";
        }
        else if (!aceptaTerminos)
        {
            error = "Para seguir, aceptá los términos.";
        }
        else
        {
            bool registrado = _bd.RegistrarUsuario(nombre, apellido, email, contrasena);
            if (registrado)
            {
                Usuario? usuario = _bd.IniciarSesion(email, contrasena);
                if (usuario != null)
                {
                    GuardarUsuarioEnSesion(usuario);
                    return RedirectToAction("Index", "Inicio");
                }
            }
            error = "Ya existe una cuenta con ese email.";
        }

        ViewBag.Error = error;
        ViewBag.Nombre = nombre;
        ViewBag.Apellido = apellido;
        ViewBag.Email = email;
        ViewBag.AceptaTerminos = aceptaTerminos;
        return View();
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    private void GuardarUsuarioEnSesion(Usuario usuario)
    {
        HttpContext.Session.SetString("IdUsuario", usuario.IdUsuario.ToString());
        HttpContext.Session.SetString("Nombre", usuario.Nombre);
    }
}
