using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<List<UserDto?>> GetAllUsers()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.SysAdmin.ToString())
            {
                return _userService.GetAllUsers();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("[action]/{id}")]
        public ActionResult<UserDto?> GetUserById([FromRoute] int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.SysAdmin.ToString())
            {
                try
                {
                    return _userService.GetUserById(id);
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

        [Authorize]
        [HttpGet("[action]/{role}")]
        public ActionResult<List<UserDto>> GetUsersByRole([FromRoute] UserRole role)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.SysAdmin.ToString())
            {
                return _userService.GetUsersByRole(role);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("[action]")]
        public ActionResult<UserDto> CreateNewUser([FromBody] UserCreateRequest userCreateRequest)
        {
            try
            {
                return _userService.CreateNewUser(userCreateRequest);
            }
            catch (Exception)
            {
                return Conflict("El email que intenta utilizar ya existe en la base de datos.");
            }
        }

        [Authorize]
        [HttpPut("[action]")]
        public ActionResult ModifyUserData([FromBody] UserUpdateRequest userUpdateRequest)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            _userService.ModifyUserData(userId, userUpdateRequest);
            return Ok();
        }

        [Authorize]
        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.SysAdmin.ToString())
            {
                try
                {
                    _userService.DeleteUser(id);
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

        [Authorize]
        [HttpPost("[action]")]
        public ActionResult<UserDto> CreateNewAdmin([FromBody] UserCreateRequest userCreateRequest)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == UserRole.SysAdmin.ToString())
            {
                try
                {
                    return _userService.CreateNewAdmin(userCreateRequest);
                }
                catch (Exception)
                {
                    return Conflict("El email que intenta utilizar ya existe en la base de datos.");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
