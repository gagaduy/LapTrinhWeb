using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buoi5.Models;

public class Book
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    [Display(Name = "Ten sach")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    [Display(Name = "Tac gia")]
    public string Author { get; set; } = string.Empty;

    [Range(0, 999999999)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Gia")]
    public decimal Price { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Mo ta")]
    public string? Description { get; set; }

    [StringLength(100)]
    [Display(Name = "Anh")]
    public string? Image { get; set; }

    [Display(Name = "Chu de")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
