using AuthBaseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId);

        Task CreateAsync(RefreshToken token);
        Task RevokeAsync(RefreshToken token);
    }
}
