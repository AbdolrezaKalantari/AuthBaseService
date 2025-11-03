using AuthBaseService.Application.Interfaces;
using AuthBaseService.Domain.Entities;
using AuthBaseService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthBaseService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public TokenController(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessToken([FromBody] string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token == null || token.Expiration < DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token.");

            var user = await _userRepository.GetByIdAsync(token.UserId);
            if (user == null || !user.EmailConfirmed)
                return Unauthorized("User not found or email not confirmed.");

            var newAccessToken = _tokenService.GenerateToken(user);

            // اختیاری: تولید Refresh Token جدید و لغو قبلی
            await _refreshTokenRepository.RevokeAsync(token);
            var newRefreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };
            await _refreshTokenRepository.CreateAsync(newRefreshToken);

            return Ok(new
            {
                accessToken = newAccessToken.AccessToken,
                accessTokenExpires = newAccessToken.Expiration,
                refreshToken = newRefreshToken.Token,
                refreshTokenExpires = newRefreshToken.Expiration
            });
        }
    }
}
