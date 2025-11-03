using AuthBaseService.Application.Interfaces;
using AuthBaseService.Application.Services;
using AuthBaseService.Data.ApplicationContext;
using AuthBaseService.Data.Repositories;
using AuthBaseService.Domain.Entities;
using AuthBaseService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            //repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationTokenRepository, EmailConfirmationTokenRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
