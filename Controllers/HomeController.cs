using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Buoi4_B2.Models;

namespace Buoi4_B2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITodoRepository _todoRepository;

    public HomeController(ILogger<HomeController> logger, ITodoRepository todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(TodoList));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View(new TodoItem());
    }

    [HttpPost]
    public IActionResult Create(TodoItem item)
    {
        _todoRepository.Add(item);

        return RedirectToAction(nameof(TodoList));
    }

    public IActionResult Edit(int id)
    {
        var item = _todoRepository.GetById(id) ?? new TodoItem();

        return View(item);
    }

    public IActionResult Details(int id)
    {
        var item = _todoRepository.GetById(id) ?? new TodoItem();

        return View(item);
    }

    [HttpPost]
    public IActionResult Edit(TodoItem item)
    {
        _todoRepository.Update(item);

        return RedirectToAction(nameof(TodoList));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _todoRepository.Delete(id);

        return RedirectToAction(nameof(TodoList));
    }

    public IActionResult TodoList()
    {
        return View(_todoRepository.GetAll());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
