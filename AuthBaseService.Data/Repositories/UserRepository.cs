using AuthBaseService.Domain.Entities;
using AuthBaseService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBaseService.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User?> GetByIdAsync(int id) => await _userManager.FindByIdAsync(id.ToString());
        public async Task<User?> GetByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);
        public async Task<User?> GetByUsernameAsync(string username) => await _userManager.FindByNameAsync(username);
        public async Task<IEnumerable<User>> GetAllAsync() => _userManager.Users.ToList();

        public async Task CreateAsync(User user) => await _userManager.CreateAsync(user);
        public async Task UpdateAsync(User user) => await _userManager.UpdateAsync(user);
        public async Task DeleteAsync(User user) => await _userManager.DeleteAsync(user);
    }
}
