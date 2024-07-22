using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public List<OrderDto?> GetAllOrders()
        {
            var orderslist = _orderRepository.GetAllOrders();
            if (orderslist == null || !orderslist.Any())
                throw new NotFoundException();

            return OrderDto.CreateList(orderslist);
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
                throw new NotFoundException(nameof(Order), id);

            return OrderDto.Create(order);
        }

        public OrderDto CreateNewOrder(int userId, OrderCreateRequest orderCreateRequest)
        {
            var user = _userRepository.GetById(userId);
            var product = _productRepository.GetProductById(orderCreateRequest.ProductId);
            
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), orderCreateRequest.ProductId);
            }

            if (product.Category == null)
            {
                throw new NotFoundException(nameof(Category), product.Id);
            }

            if (orderCreateRequest.UnitsAmount <= 0)
            {
                throw new Exception();
            }

            var order = new Order
            {
                Product = product,
                User = user,
                UnitsAmount = orderCreateRequest.UnitsAmount,
            };

            _orderRepository.Add(order);
            return OrderDto.Create(order);
        }

        public void ModifyOrderData(int id, OrderUpdateRequest orderUpdateRequest)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                throw new NotFoundException(nameof(Order), id);

            if (orderUpdateRequest.State != order.State) order.State = orderUpdateRequest.State;

            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                throw new NotFoundException(nameof(Order), id);

            _orderRepository.Delete(order);
        }

        public List<OrderDto?> GetOrdersByUser(int userId)
        {
            var orderslist = _orderRepository.GetOrdersByUser(userId);
            if (orderslist == null || !orderslist.Any())
                throw new NotFoundException(nameof(Order), userId);

            return OrderDto.CreateList(orderslist);
        }

        public List<OrderDto?> GetOrdersByProduct(int productId)
        {
            var orderslist = _orderRepository.GetOrdersByProduct(productId);
            if (orderslist == null || !orderslist.Any())
                throw new NotFoundException(nameof(Order), productId);

            return OrderDto.CreateList(orderslist);
        }

        public int? GetOrderUnitsAmount(int orderId)
        {
            int? unitAmount = _orderRepository.GetOrderUnitsAmount(orderId);
            if (unitAmount == null)
                throw new NotFoundException(nameof(Order), orderId);

            return unitAmount;
        }

        public List<OrderDto?> GetOrdersByState(OrderState state)
        {
            var orderslist = _orderRepository.GetOrdersByState(state);
            if (orderslist == null || !orderslist.Any())
                throw new NotFoundException(nameof(Order), state);

            return OrderDto.CreateList(orderslist);
        }
    }
}
