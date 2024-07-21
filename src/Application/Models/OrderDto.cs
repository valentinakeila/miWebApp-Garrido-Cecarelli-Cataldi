using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        //public Product Product { get; set; }
        //public User User { get; set; }
        public string? ProductName { get; set; }
        public string? UserName { get; set; }
        public int UnitsAmount { get; set; }
        public decimal TotalPrice { get; private set; }
        public string? CreationDate { get; set; }
        public OrderState State { get; set; }

        public static OrderDto Create(Order order)
        {
            
            var dto = new OrderDto();
            dto.Id = order.Id;
            //dto.Product = order.Product;
            //dto.User = order.User;
            dto.ProductName = order.Product.Name;
            dto.UserName = $"{order.User.FirstName} {order.User.LastName}";
            dto.UnitsAmount = order.UnitsAmount;
            dto.TotalPrice = order.UnitsAmount * order.Product.Price;
            dto.CreationDate = order.CreationDate.ToShortDateString();
            dto.State = order.State;

            return dto;
        }
        public static List<OrderDto?> CreateList(IEnumerable<Order> orders)
        {
            List<OrderDto?> listDto = [];

            foreach (var o in orders)
            {
                listDto.Add(Create(o));
            }

            return listDto;
        }
    }
}
