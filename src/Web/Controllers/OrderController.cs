using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Domain.Exceptions;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using Domain.Entities;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<OrderDto?> GetOrderById([FromRoute] int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("[action]")]
        public ActionResult<OrderDto> CreateNewOrder([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Client.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyOrderData([FromRoute] int id, [FromBody] OrderUpdateRequest orderUpdateRequest)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteOrder([FromRoute] int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }


        [HttpGet("[Action]/{userId}")]
        public ActionResult<List<OrderDto?>> GetOrdersByUser([FromRoute] int userId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("[Action]/{productId}")]
        public ActionResult<List<OrderDto?>> GetOrdersByProduct([FromRoute] int productId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("[action]/{orderId}")]
        public ActionResult<int?> GetOrderUnitsAmount([FromRoute] int orderId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.Admin.ToString() || userRole == UserRole.SysAdmin.ToString())
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
            else
            {
                return Unauthorized();
            }
        }

    }
}

//int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");
//var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
