using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buoi6.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Range(0, 1000000000)]
        public decimal Price { get; set; }
        
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        
        public List<ProductImage>? Images { get; set; }
    }
}
