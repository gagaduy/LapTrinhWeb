using KiemTraGiuaKy.Data;
using KiemTraGiuaKy.Models;
using KiemTraGiuaKy.ViewModels.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KiemTraGiuaKy.Controllers;

[Authorize(Roles = "Admin")]
[Route("admin/courses")]
public class AdminCoursesController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminCoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses
            .Include(course => course.Category)
            .AsNoTracking()
            .OrderBy(course => course.Name)
            .ToListAsync();

        return View(courses);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        return View(await BuildFormViewModelAsync(new CourseFormViewModel()));
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormViewModelAsync(model));
        }

        var course = new Course
        {
            Name = model.Name,
            Image = model.Image,
            Credits = model.Credits,
            Lecturer = model.Lecturer,
            CategoryId = model.CategoryId
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Da them hoc phan.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        return View(await BuildFormViewModelAsync(new CourseFormViewModel
        {
            Id = course.Id,
            Name = course.Name,
            Image = course.Image,
            Credits = course.Credits,
            Lecturer = course.Lecturer,
            CategoryId = course.CategoryId
        }));
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CourseFormViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildFormViewModelAsync(model));
        }

        var course = await _context.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        course.Name = model.Name;
        course.Image = model.Image;
        course.Credits = model.Credits;
        course.Lecturer = model.Lecturer;
        course.CategoryId = model.CategoryId;

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Da cap nhat hoc phan.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Da xoa hoc phan.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<CourseFormViewModel> BuildFormViewModelAsync(CourseFormViewModel model)
    {
        model.Categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .Select(category => new SelectListItem(category.Name, category.Id.ToString()))
            .ToListAsync();

        return model;
    }
}
