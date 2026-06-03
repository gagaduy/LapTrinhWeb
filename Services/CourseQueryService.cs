using KiemTraGiuaKy.Models;

namespace KiemTraGiuaKy.Services;

public class CourseQueryService
{
    public PagedResult<Course> SearchAndPaginate(IEnumerable<Course> courses, string? searchTerm, int page, int pageSize)
    {
        var query = courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(course => course.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        var totalItems = query.Count();
        var safePageSize = Math.Max(1, pageSize);
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)safePageSize));
        var safePage = Math.Min(Math.Max(1, page), totalPages);

        var items = query
            .OrderBy(course => course.Name)
            .Skip((safePage - 1) * safePageSize)
            .Take(safePageSize)
            .ToList();

        return new PagedResult<Course>
        {
            Items = items,
            Page = safePage,
            PageSize = safePageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }
}
