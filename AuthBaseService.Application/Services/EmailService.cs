using AuthBaseService.Application.Interfaces;
using AuthBaseService.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendConfirmationEmailAsync(User user, string token)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["mail:from"]));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Confirm your email";

            var confirmationLink = $"{_configuration["App:BaseUrl"]}/api/auth/confirm?token={token}";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<p>Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.</p>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Mail:SmtpHost"], int.Parse(_configuration["Mail:SmtpPort"]!), SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
