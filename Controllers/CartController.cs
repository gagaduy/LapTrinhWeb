using Microsoft.AspNetCore.Mvc;

namespace Buoi5.Controllers;

public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
