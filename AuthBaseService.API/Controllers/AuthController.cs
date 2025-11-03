using AuthBaseService.Application.DTOs;
using AuthBaseService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthBaseService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), result); //201 Create
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(token); //200 ok
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto dto)
        {
            var success = await _authService.ConfirmEmailAsync(dto);
            if (!success)
            {
                return BadRequest("Invalid or expired token.");
            }
            return Ok("Email confirmed successfully.");
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmailViaLink([FromQuery] string token)
        {
            var success = await _authService.ConfirmEmailAsync(new ConfirmEmailDto { Token = token });
            if (!success)
                return BadRequest("Invalid or expired token.");

            return Ok("Email confirmed successfully via link.");
        }
    }
}