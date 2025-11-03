using AuthBaseService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<TokenDto> LoginAsync(LoginDto loginDto);
        Task<bool> ConfirmEmailAsync(ConfirmEmailDto dto);
    }
}
