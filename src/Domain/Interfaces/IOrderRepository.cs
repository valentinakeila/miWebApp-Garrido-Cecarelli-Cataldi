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

        List<Order?> GetOrderByUser(int userId);

        List<Order?> GetOrderByProduct(int productId);

        int? GetOrderUnitsAmount(int orderId);
    }
    
}
