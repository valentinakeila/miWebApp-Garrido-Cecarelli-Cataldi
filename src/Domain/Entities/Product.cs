using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "numeric(7,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ImageUrl { get; set; }

        public Product()
        {
            
        }

        public Product(string name, decimal price, string description, Category category, string imageUrl)
        {
            Name = name;
            Price = price;
            Description = description;
            Category = category;
            ImageUrl = imageUrl;
        }
    }
}
