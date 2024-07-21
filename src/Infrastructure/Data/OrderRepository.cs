using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data
{
    public class OrderRepository(ApplicationContext context) : EfRepository<Order>(context), IOrderRepository
    {
        public Order? GetOrderByProduct(int productId)
        {
            var query = _context.Orders.Where(o => o.Product.Id == productId);
            return query.FirstOrDefault();
        }

        public Order? GetOrderByUser(int userId)
        {
            var query = _context.Orders.Where(o => o.User.Id == userId);
            return query.FirstOrDefault();
        }

        public int? GetOrderUnitsAmount(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            
            return order.UnitsAmount;
            
        }
    }
}
