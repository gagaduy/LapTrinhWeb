namespace KiemTraGiuaKy.ViewModels.Courses;

public class CourseCardViewModel
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Lecturer { get; init; } = string.Empty;
    public int Credits { get; init; }
    public string Image { get; init; } = string.Empty;
    public string CategoryName { get; init; } = string.Empty;
    public bool IsEnrolled { get; init; }
}
