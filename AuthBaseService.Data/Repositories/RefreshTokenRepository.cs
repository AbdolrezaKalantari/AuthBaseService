using AuthBaseService.Data.ApplicationContext;
using AuthBaseService.Domain.Entities;
using AuthBaseService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(int userId)
        {
            return await _context.RefreshTokens
             .Where(rt => rt.UserId == userId && !rt.IsRevoked)
             .ToListAsync();
        }

        public async Task RevokeAsync(RefreshToken token)
        {
            token.IsRevoked = true;
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
