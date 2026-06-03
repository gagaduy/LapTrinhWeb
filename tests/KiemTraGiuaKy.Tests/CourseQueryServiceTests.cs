using FluentAssertions;
using KiemTraGiuaKy.Models;
using KiemTraGiuaKy.Services;

namespace KiemTraGiuaKy.Tests;

public class CourseQueryServiceTests
{
    [Fact]
    public void SearchAndPaginate_should_filter_by_name_and_return_five_items_per_page()
    {
        var courses = Enumerable.Range(1, 12)
            .Select(index => new Course
            {
                Id = index,
                Name = index <= 7 ? $"Lap trinh Web {index}" : $"Co so du lieu {index}",
                Credits = 3,
                Lecturer = $"Giang vien {index}",
                Image = $"/images/{index}.jpg",
                CategoryId = 1
            })
            .ToList();

        var service = new CourseQueryService();

        var result = service.SearchAndPaginate(courses, "lap trinh", page: 2, pageSize: 5);

        result.TotalItems.Should().Be(7);
        result.TotalPages.Should().Be(2);
        result.Items.Should().HaveCount(2);
        result.Items.Select(course => course.Name).Should().OnlyContain(name => name.Contains("Lap trinh", StringComparison.OrdinalIgnoreCase));
    }
}
