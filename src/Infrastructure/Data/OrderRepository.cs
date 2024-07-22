using Application.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data
{
    public class OrderRepository(ApplicationContext context) : EfRepository<Order>(context), IOrderRepository
    {
        public List<Order> GetAllOrders()
        {
            return _context.Orders
                 .Include(o => o.Product.Category)
                 .Include(o => o.User)
                 .ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders
                 .Include(o => o.Product)
                 .Include(o => o.User)
                 .Where(o => o.Id == id)
                 .FirstOrDefault();
        }

        public List<Order?> GetOrdersByProduct(int productId)
        {
            var query = _context.Orders
                .Include( o => o.Product)
                .Include( o => o.User)
                .Where(o => o.Product.Id == productId);

            return query.ToList();
        }

        public List<Order?> GetOrdersByUser(int userId)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Product)
                .Where(o => o.User.Id == userId);
            return query.ToList();
        }

        public int? GetOrderUnitsAmount(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            
            return order.UnitsAmount;
            
        }

        public List<Order?> GetOrdersByState(OrderState state)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Product.Category)
                .Where(o => o.State == state);
            return query.ToList();
        }
    }
}
