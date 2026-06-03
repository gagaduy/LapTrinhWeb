using KiemTraGiuaKy.Models;

namespace KiemTraGiuaKy.Services;

public class EnrollmentService
{
    public Enrollment Enroll(ICollection<Enrollment> enrollments, string userId, int courseId, DateTime enrollDate)
    {
        if (enrollments.Any(enrollment => enrollment.UserId == userId && enrollment.CourseId == courseId))
        {
            throw new InvalidOperationException("Course already enrolled.");
        }

        var enrollment = new Enrollment
        {
            UserId = userId,
            CourseId = courseId,
            EnrollDate = enrollDate
        };

        enrollments.Add(enrollment);
        return enrollment;
    }

    public bool Unenroll(ICollection<Enrollment> enrollments, string userId, int courseId)
    {
        var enrollment = enrollments.FirstOrDefault(item => item.UserId == userId && item.CourseId == courseId);
        if (enrollment is null)
        {
            return false;
        }

        enrollments.Remove(enrollment);
        return true;
    }
}
