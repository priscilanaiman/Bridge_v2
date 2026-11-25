using Microsoft.AspNetCore.Mvc;

namespace Bridge.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
