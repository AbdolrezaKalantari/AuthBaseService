using AuthBaseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Domain.Interfaces
{
    public interface IEmailConfirmationTokenRepository
    {
        Task<EmailConfirmationToken?> GetByTokenAsync(string token);
        Task<IEnumerable<EmailConfirmationToken>> GetByUserIdAsync(int userId);

        Task CreateAsync(EmailConfirmationToken token);
        Task MarkAsUsedAsync(EmailConfirmationToken token);
    }
}
