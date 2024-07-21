using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Column(TypeName = "numeric(3)")]
        public int UnitsAmount { get; set; }

        [Column(TypeName = "numeric(8,2)")]
        public decimal TotalPrice { get; private set; }

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; private set; } //= DateTime.Now;

        [Column(TypeName = "nvarchar(20)")]
        public OrderState State { get; set; } //= OrderState.Pendent;

        public Order()
        {

        }

        public Order(Product product, User user, int unitsAmount)
        {
            Product = product;
            User = user;
            UnitsAmount = unitsAmount;
            TotalPrice = this.UnitsAmount * this.Product.Price;
            CreationDate = DateTime.Now;
            State = OrderState.Pendent;
        }
    }
}
