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
            try
            {
                return _orderService.GetAllOrders();
            }
            catch (NotFoundException)
            {

                return NotFound("No existen órdenes almacenadas");
            }
            
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
            try
            {
                return _orderService.CreateNewOrder(orderCreateRequest);
            }
            catch (NotFoundException)
            {
                return NotFound("El usuario o producto especificado no existe");
            }
            catch (Exception)
            {
                return Conflict("La cantidad de unidades debe ser mayor a cero");
            }
        }

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyOrderData([FromRoute] int id, [FromBody] OrderUpdateRequest orderUpdateRequest)
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
        public ActionResult<List<OrderDto?>> GetOrdersByUser([FromRoute] int userId)
        {
            try
            {
                return _orderService.GetOrdersByUser(userId);
            }
            catch (NotFoundException)
            {
                return NotFound("El user especificado no existe");
            }
        }

        [HttpGet("[Action]/{productId}")]
        public ActionResult<List<OrderDto?>> GetOrdersByProduct([FromRoute] int productId)
        {
            try
            {
                return _orderService.GetOrdersByProduct(productId);
            }
            catch (NotFoundException)
            {
                return NotFound("El id especificado no existe");
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
