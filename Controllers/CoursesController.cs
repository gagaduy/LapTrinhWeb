using KiemTraGiuaKy.Data;
using KiemTraGiuaKy.Models;
using KiemTraGiuaKy.Services;
using KiemTraGiuaKy.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KiemTraGiuaKy.Controllers;

[Route("courses")]
public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly CourseQueryService _courseQueryService;

    public CoursesController(ApplicationDbContext context, CourseQueryService courseQueryService)
    {
        _context = context;
        _courseQueryService = courseQueryService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? searchTerm, int page = 1)
    {
        var courses = await _context.Courses
            .Include(course => course.Category)
            .AsNoTracking()
            .ToListAsync();

        var enrolledCourseIds = new HashSet<int>();
        var isStudent = User.IsInRole("Student");
        if (isStudent)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                var courseIds = await _context.Enrollments
                    .Where(enrollment => enrollment.UserId == userId)
                    .Select(enrollment => enrollment.CourseId)
                    .ToListAsync();
                enrolledCourseIds = courseIds.ToHashSet();
            }
        }

        var mappedCourses = courses.Select(course => new Course
        {
            Id = course.Id,
            Name = course.Name,
            Credits = course.Credits,
            Lecturer = course.Lecturer,
            Image = course.Image,
            CategoryId = course.CategoryId,
            Category = course.Category,
            Enrollments = course.Enrollments
        }).ToList();

        var pagedCourses = _courseQueryService.SearchAndPaginate(mappedCourses, searchTerm, page, 5);
        var cards = pagedCourses.Items
            .Select(course => new CourseCardViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Lecturer = course.Lecturer,
                Credits = course.Credits,
                Image = course.Image,
                CategoryName = course.Category?.Name ?? string.Empty,
                IsEnrolled = enrolledCourseIds.Contains(course.Id)
            })
            .ToList();

        var paging = new PagedResult<CourseCardViewModel>
        {
            Items = cards,
            Page = pagedCourses.Page,
            PageSize = pagedCourses.PageSize,
            TotalItems = pagedCourses.TotalItems,
            TotalPages = pagedCourses.TotalPages
        };

        return View(new CourseListViewModel
        {
            Courses = cards,
            SearchTerm = searchTerm ?? string.Empty,
            IsStudent = isStudent,
            Paging = paging,
            Title = "Danh sach hoc phan"
        });
    }
}
