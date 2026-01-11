using HashBank.Services;
using HashBank.Services.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HashBank.Api.Controllers
{
    [ApiController] // al usar esti no necesito el if(ModelState.IsValid)
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        public IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto createUserDto)
        {
            try
            {
                int newUserId = await _userService.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(GetUserById), new { id = newUserId }, new { id = newUserId });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) 
        { 
            return Ok(); 
        }
    }
}
