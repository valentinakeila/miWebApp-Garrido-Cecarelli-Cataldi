using Application.Models.Request;
using Application.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        OrderDto? GetOrderById(int id);

        List<OrderDto?> GetAllOrders();

        OrderDto CreateNewOrder(OrderCreateRequest orderCreateRequest);

        void ModifyOrderData(int id, OrderUpdateRequest orderUpdateRequest);

        void DeleteOrder(int id);

        List<OrderDto?> GetOrdersByUser(int userId);

        List<OrderDto?> GetOrdersByProduct(int productId);

        int? GetOrderUnitsAmount(int orderId);

    }
}
