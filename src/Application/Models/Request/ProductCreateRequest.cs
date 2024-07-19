using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        //public int CategoryId { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
