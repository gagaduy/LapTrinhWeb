using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KiemTraGiuaKy.ViewModels.Courses;

public class CourseFormViewModel
{
    public int? Id { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Ten hoc phan")]
    public string Name { get; set; } = string.Empty;

    [Required, Url, StringLength(500)]
    [Display(Name = "Hinh anh")]
    public string Image { get; set; } = string.Empty;

    [Range(1, 10)]
    [Display(Name = "So tin chi")]
    public int Credits { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Giang vien")]
    public string Lecturer { get; set; } = string.Empty;

    [Display(Name = "Danh muc")]
    public int CategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
}
