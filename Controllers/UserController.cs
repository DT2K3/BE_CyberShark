using BE_cybershark.Models;
using BE_CyberShark.Services;
using BE_CyberShark.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_CyberShark.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel registerviewmodel)
        {
            try
            {
                User? user = await _userService.Register(registerviewmodel);

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Registration failed: " + ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                string jwtToken = await _userService.Login(loginViewModel);
                return Ok(new { jwtToken });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "Login failed", message = ex.Message });
            }
        }

    }
}
