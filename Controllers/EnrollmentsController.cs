using KiemTraGiuaKy.Data;
using KiemTraGiuaKy.Services;
using KiemTraGiuaKy.ViewModels.Courses;
using KiemTraGiuaKy.ViewModels.Enrollments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KiemTraGiuaKy.Controllers;

[Authorize(Roles = "Student")]
[Route("enroll")]
public class EnrollmentsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentsController(ApplicationDbContext context, EnrollmentService enrollmentService)
    {
        _context = context;
        _enrollmentService = enrollmentService;
    }

    [HttpPost("{courseId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int courseId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        var courseExists = await _context.Courses.AnyAsync(course => course.Id == courseId);
        if (!courseExists)
        {
            return NotFound();
        }

        var enrollments = await _context.Enrollments
            .Where(enrollment => enrollment.UserId == userId || enrollment.CourseId == courseId)
            .ToListAsync();

        try
        {
            var created = _enrollmentService.Enroll(enrollments, userId, courseId, DateTime.UtcNow);
            _context.Enrollments.Add(created);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Dang ky hoc phan thanh cong.";
        }
        catch (InvalidOperationException)
        {
            TempData["ErrorMessage"] = "Ban da dang ky hoc phan nay.";
        }

        return RedirectToAction("Index", "Courses");
    }

    [HttpPost("{courseId:int}/cancel")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int courseId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(item => item.UserId == userId && item.CourseId == courseId);
        if (enrollment is null)
        {
            TempData["ErrorMessage"] = "Khong tim thay hoc phan da dang ky.";
            return RedirectToAction(nameof(MyCourses));
        }

        var enrollments = await _context.Enrollments
            .Where(enrollment => enrollment.UserId == userId)
            .ToListAsync();

        if (_enrollmentService.Unenroll(enrollments, userId, courseId))
        {
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Da huy dang ky hoc phan.";
        }
        else
        {
            TempData["ErrorMessage"] = "Khong tim thay hoc phan da dang ky.";
        }

        return RedirectToAction(nameof(MyCourses));
    }

    [HttpGet("my-courses")]
    public async Task<IActionResult> MyCourses()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        var courses = await _context.Enrollments
            .Where(enrollment => enrollment.UserId == userId)
            .OrderByDescending(enrollment => enrollment.EnrollDate)
            .Select(enrollment => new CourseCardViewModel
            {
                Id = enrollment.Course!.Id,
                Name = enrollment.Course.Name,
                Lecturer = enrollment.Course.Lecturer,
                Credits = enrollment.Course.Credits,
                Image = enrollment.Course.Image,
                CategoryName = enrollment.Course.Category!.Name,
                IsEnrolled = true
            })
            .ToListAsync();

        return View(new MyCoursesViewModel { Courses = courses });
    }
}
