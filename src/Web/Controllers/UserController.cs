using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Enums;
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

        [HttpGet("[action]")]
        public ActionResult<List<UserDto?>> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<UserDto?> GetUserById([FromRoute] int id)
        {
            return _userService.GetUserById(id);
        }

        [HttpGet("[action]/{role}")]
        public ActionResult<List<UserDto>> GetUsersByRole([FromRoute] UserRole role)
        {
            return _userService.GetUsersByRole(role);
        }

        [HttpPost("[action]")]
        public ActionResult<UserDto> CreateNewUser([FromBody] UserCreateRequest userCreateRequest)
        {
            return _userService.CreateNewUser(userCreateRequest);
        }

        [HttpPut("[action]/{id}")]
        public ActionResult ModifyUserData([FromRoute] int id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            _userService.ModifyUserData(id, userUpdateRequest);
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
