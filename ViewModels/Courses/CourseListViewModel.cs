using KiemTraGiuaKy.Services;

namespace KiemTraGiuaKy.ViewModels.Courses;

public class CourseListViewModel
{
    public IReadOnlyList<CourseCardViewModel> Courses { get; init; } = Array.Empty<CourseCardViewModel>();
    public string SearchTerm { get; init; } = string.Empty;
    public PagedResult<CourseCardViewModel> Paging { get; init; } = new();
    public bool IsStudent { get; init; }
    public string Title { get; init; } = string.Empty;
}
