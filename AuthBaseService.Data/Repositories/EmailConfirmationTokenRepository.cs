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
    public class EmailConfirmationTokenRepository : IEmailConfirmationTokenRepository
    {
        private readonly AppDbContext _context;
        public EmailConfirmationTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<EmailConfirmationToken?> GetByTokenAsync(string token) =>
         await _context.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Token == token);

        public async Task<IEnumerable<EmailConfirmationToken>> GetByUserIdAsync(int userId) =>
            await _context.EmailConfirmationTokens.Where(t => t.UserId == userId).ToListAsync();

        public async Task CreateAsync(EmailConfirmationToken token)
        {
            await _context.EmailConfirmationTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
        public async Task MarkAsUsedAsync(EmailConfirmationToken token)
        {
            token.IsUsed = true;
            _context.EmailConfirmationTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
