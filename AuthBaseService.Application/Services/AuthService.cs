using AuthBaseService.Application.DTOs;
using AuthBaseService.Application.Interfaces;
using AuthBaseService.Domain.Entities;
using AuthBaseService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailConfirmationTokenRepository _tokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        public AuthService(
         IUserRepository userRepository,
         IEmailConfirmationTokenRepository tokenRepository,
         ITokenService tokenService,
         IEmailService emailService,
         UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenService = tokenService;
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task<bool> ConfirmEmailAsync(ConfirmEmailDto dto)
        {
            var token = await _tokenRepository.GetByTokenAsync(dto.Token);
            if (token == null || token.IsUsed || token.Expiration < DateTime.UtcNow)
            {
                return false;
            }

            var user = await _userRepository.GetByIdAsync(token.UserId);
            if (user == null)
            {
                return false;
            }
            user.EmailConfirmed = true;
            await _userRepository.UpdateAsync(user);
            await _tokenRepository.MarkAsUsedAsync(token);
            return true;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
             
                return new TokenDto { Response = "Incorrect email or password." };

            if (!user.EmailConfirmed)
              
                return new TokenDto { Response = "Your email has not been verified yet." };

            return _tokenService.GenerateToken(user);
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                
                return new UserDto { Response = "Email already registered!" };
            }

            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {           
                return new UserDto { Response = "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)) };
            }
            var token = Guid.NewGuid().ToString();
            var confirmation = new EmailConfirmationToken
            {
                UserId = user.Id,
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(24)
            };
            await _tokenRepository.CreateAsync(confirmation);
            await _emailService.SendConfirmationEmailAsync(user, token);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
        }
    }
}
