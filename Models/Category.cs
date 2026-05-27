using System.ComponentModel.DataAnnotations;

namespace Buoi6.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public List<Product>? Products { get; set; }
    }
}
