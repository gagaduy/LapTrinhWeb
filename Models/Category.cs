using System.ComponentModel.DataAnnotations;

namespace Buoi5.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new();
}
