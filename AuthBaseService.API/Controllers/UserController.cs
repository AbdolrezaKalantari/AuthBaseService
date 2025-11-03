using AuthBaseService.Application.DTOs;
using AuthBaseService.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System.Security.Claims;

namespace AuthBaseService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out var userId))
                return BadRequest("Invalid user ID.");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var dto = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };

            return Ok(dto);
        }

       
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            var result = users.Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            });

            return Ok(result);
        }
    }
}
