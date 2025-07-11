using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.DTOs.RequestDTOs;
using DemoNLayerApi.DTOs.ResponseDTOs;
using DemoNLayerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLayerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        [Authorize(Policy = "AdminOnlyPolicy")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userRequest)
        {
            var _userRequest = new User
            {

                Name = userRequest.Name,
                Profile = new UserProfile
                {
                    Email = userRequest.Email,
                    Password = userRequest.Password
                }
            };

            await _userService.CreateUser(_userRequest);
            return Ok("User created successfully");
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
           var users = await _userService.GetUsers();
          
            return Ok(users);
        }
    }
}
