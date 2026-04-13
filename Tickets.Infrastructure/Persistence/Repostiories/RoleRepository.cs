using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Interfaces;

namespace Tickets.Infrastructure.Persistence.Repostiories
{
    public class RoleRepository : IRolePository
    {
        readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string roleName)
        {
            if (!await RoleExistsAsync(roleName))
            {
                //todo -> Crear el rol usando el RoleManager de Identity
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> RoleExistsAsync(string rolaName)
        {
            return await _roleManager.RoleExistsAsync(rolaName);
        }
    }
}
