using AuthBaseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(User user, string token);
    }
}
