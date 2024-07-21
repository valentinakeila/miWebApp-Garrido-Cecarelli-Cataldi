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
            var orders = _orderRepository.List();

            return OrderDto.CreateList(orders);
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                throw new NotFoundException(nameof(Order), id);

            return OrderDto.Create(order);
        }

        public OrderDto CreateNewOrder(OrderCreateRequest orderCreateRequest)
        {
            var user = _userRepository.GetById(orderCreateRequest.UserId);
            var product = _productRepository.GetById(orderCreateRequest.ProductId);

            if (user == null)
            {
                
                throw new NotFoundException(nameof(User), orderCreateRequest.UserId);

            }else if (product == null)
            {
                throw new NotFoundException(nameof(Product), orderCreateRequest.ProductId);
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

            if (orderUpdateRequest.UnitsAmount != order.UnitsAmount) order.UnitsAmount = orderUpdateRequest.UnitsAmount;


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
            var order = _orderRepository.GetOrdersByUser(userId);
            if (order == null)
                throw new NotFoundException(nameof(Order), userId);

            return OrderDto.CreateList(order);
        }

        public List<OrderDto?> GetOrdersByProduct(int productId)
        {
            var order = _orderRepository.GetOrdersByProduct(productId);
            if (order == null)
                throw new NotFoundException(nameof(Order), productId);

            return OrderDto.CreateList(order);
        }

        public int? GetOrderUnitsAmount(int orderId)
        {
            int? unitAmount = _orderRepository.GetOrderUnitsAmount(orderId);
            if (unitAmount == null)
                throw new NotFoundException(nameof(Order), orderId);

            return unitAmount;
        }
    }
}
