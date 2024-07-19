using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
            return _userService.GetAllUsers();
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<UserDto?> GetUserById([FromRoute] int id)
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

        [HttpGet("[action]/{role}")]
        public ActionResult<List<UserDto>> GetUsersByRole([FromRoute] UserRole role)
        {
            return _userService.GetUsersByRole(role);
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

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyUserData([FromRoute] int id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            try
            {
                _userService.ModifyUserData(id, userUpdateRequest);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound("El Id especificado no existe");
            }
            catch (Exception)
            {
                return Conflict("El email que intenta utilizar ya existe en la base de datos.");
            }

        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
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
    }
}
