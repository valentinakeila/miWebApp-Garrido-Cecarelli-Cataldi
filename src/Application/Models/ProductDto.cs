using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int CategoryId{ get; set; }
        public string ImageUrl { get; set; }

        public static ProductDto Create(Product product)
        {
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.Category.Id,
                ImageUrl = product.ImageUrl
            };

            return dto;
        }

        public static List<ProductDto> CreateList(List<Product> products)
        {
            var productList = new List<ProductDto>();
            foreach (var product in products)
            {
                productList.Add(Create(product));
            }
            return productList;
        }
    }
}
