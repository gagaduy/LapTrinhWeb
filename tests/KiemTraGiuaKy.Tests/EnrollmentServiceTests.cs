using FluentAssertions;
using KiemTraGiuaKy.Models;
using KiemTraGiuaKy.Services;

namespace KiemTraGiuaKy.Tests;

public class EnrollmentServiceTests
{
    [Fact]
    public void Enroll_should_add_a_new_enrollment_when_course_is_not_registered()
    {
        var enrollments = new List<Enrollment>();
        var service = new EnrollmentService();

        var created = service.Enroll(enrollments, "student-1", 10, new DateTime(2026, 6, 3, 8, 0, 0, DateTimeKind.Utc));

        created.UserId.Should().Be("student-1");
        created.CourseId.Should().Be(10);
        enrollments.Should().ContainSingle();
    }

    [Fact]
    public void Enroll_should_not_duplicate_existing_registration()
    {
        var enrollments = new List<Enrollment>
        {
            new() { Id = 1, UserId = "student-1", CourseId = 10, EnrollDate = new DateTime(2026, 6, 3, 8, 0, 0, DateTimeKind.Utc) }
        };

        var service = new EnrollmentService();
        var action = () => service.Enroll(enrollments, "student-1", 10, new DateTime(2026, 6, 3, 9, 0, 0, DateTimeKind.Utc));

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Unenroll_should_remove_existing_registration()
    {
        var enrollments = new List<Enrollment>
        {
            new() { Id = 1, UserId = "student-1", CourseId = 10, EnrollDate = new DateTime(2026, 6, 3, 8, 0, 0, DateTimeKind.Utc) }
        };

        var service = new EnrollmentService();

        var removed = service.Unenroll(enrollments, "student-1", 10);

        removed.Should().BeTrue();
        enrollments.Should().BeEmpty();
    }
}
