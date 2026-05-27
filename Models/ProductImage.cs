using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buoi6.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; } = string.Empty;
        
        [Required]
        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
