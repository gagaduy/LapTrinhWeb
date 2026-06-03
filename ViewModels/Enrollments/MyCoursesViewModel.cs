using KiemTraGiuaKy.ViewModels.Courses;

namespace KiemTraGiuaKy.ViewModels.Enrollments;

public class MyCoursesViewModel
{
    public IReadOnlyList<CourseCardViewModel> Courses { get; init; } = Array.Empty<CourseCardViewModel>();
}
