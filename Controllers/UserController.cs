using BE_cybershark.Models;
using BE_CyberShark.Services;
using BE_CyberShark.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
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
        [HttpPost("request_resetpassword")]
        public async Task<IActionResult> SendEmail([FromBody] ResetPasswordRequestViewModel  Request)
        {
            try
            {
                // Your business logic, validation, etc., can be added here before forwarding the email
                // to the callback URL.

                // Send the email address to the callback URL
                await _emailService.SendEmailToCallback(Request);

                return Ok("Email address sent to callback successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            try
            {
                // Add logic to validate the OTP and reset the password
                await _emailService.ResetPasswordCallback(new ResetPasswordViewModel
                {
                    Email = model.Email,
                    Otp = model.Otp,
                    NewPassword = model.NewPassword
                });

                // Return a success response
                return Ok(new { Message = "Password reset successful." });
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return BadRequest(new { Message = "Password reset failed.", Error = ex.Message });
            }
        }
    }
}
