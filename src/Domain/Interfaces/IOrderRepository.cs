using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        List<Order?> GetAllOrders();
        Order? GetOrderById(int id);
        List<Order?> GetOrdersByUser(int userId);

        List<Order?> GetOrdersByProduct(int productId);

        int? GetOrderUnitsAmount(int orderId);
    }
    
}
