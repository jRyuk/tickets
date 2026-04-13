using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces;
using Tickets.Infrastructure.Identity;
using Tickets.Infrastructure.Mapping;

namespace Tickets.Infrastructure.Persistence.Repostiories
{
    public class UserRepository : IUserRepository
    {
        public readonly UserManager<AppIdentityUser> _userManager;

        public UserRepository(UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<Usuario> AddToRoleAsync(Usuario usuario, string roleName)
        {
            var userDb = await  _userManager.FindByEmailAsync(usuario.Email); 
            var result = await _userManager.AddToRoleAsync(userDb, roleName);
            return usuario;
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null && await _userManager.CheckPasswordAsync(user, password);

        }

        public async Task<Usuario> CreateUser(Usuario usuario)
        {
            var result = await _userManager.CreateAsync(usuario.ToIdentityUser(), usuario.Password);

            if (result.Succeeded)
            { 
                var newUser = await _userManager.FindByEmailAsync(usuario.Email);
                usuario.Id = new Guid(newUser.Id);
                
                return usuario;
            }

            return null;
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user?.ToDomainUser();
        }

        public async Task<List<string>> GetUserRoles(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> UserExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
