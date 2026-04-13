using System;
using System.Collections.Generic;
using System.Text;

namespace Tickets.Domain.Interfaces
{
    public interface IRolePository
    {
        /// <summary>
        /// Para revisar si un rol existe
        /// </summary>
        /// <param name="rolaName"></param>
        /// <returns></returns>
        Task<bool> RoleExistsAsync(string rolaName);

        /// <summary>
        /// Para crear roles 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> CreateRole(string roleName);


    }
}
