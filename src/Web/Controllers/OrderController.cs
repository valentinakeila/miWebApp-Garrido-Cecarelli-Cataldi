using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("[action]")]
        public ActionResult<List<OrderDto?>> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<OrderDto?> GetOrderById([FromRoute] int id)
        {
            try
            {
                return _orderService.GetOrderById(id);
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }


        [HttpPost("[action]")]
        public ActionResult<OrderDto> CreateNewOrder([FromBody] OrderCreateRequest orderCreateRequest)
        {
            return _orderService.CreateNewOrder(orderCreateRequest);
        }

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyCategoryData([FromRoute] int id, [FromBody] OrderUpdateRequest orderUpdateRequest)
        {

            try
            {
                _orderService.ModifyOrderData(id, orderUpdateRequest);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteOrder([FromRoute] int id)
        {

            try
            {
                _orderService.DeleteOrder(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }


        [HttpGet("[Action]/{userId}")]
        public ActionResult<List<OrderDto?>> GetOrderByUser([FromRoute] int userId)
        {
            try
            {
                return _orderService.GetOrderByUser(userId);
            }
            catch (NotFoundException)
            {
                return NotFound("El nombre especificado no existe");
            }
        }

        [HttpGet("[Action]/{productId}")]
        public ActionResult<List<OrderDto?>> GetOrderByProduct([FromRoute] int productId)
        {
            try
            {
                return _orderService.GetOrderByProduct(productId);
            }
            catch (NotFoundException)
            {
                return NotFound("El nombre especificado no existe");
            }
        }

        [HttpGet("[action]/{orderId}")]
        public ActionResult<int?> GetOrderUnitsAmount([FromRoute] int orderId)
        {
            try
            {
                return _orderService.GetOrderUnitsAmount(orderId);
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
        }

    }
}
