using System.Diagnostics;
using KiemTraGiuaKy.Models;
using Microsoft.AspNetCore.Mvc;

namespace KiemTraGiuaKy.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Courses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
