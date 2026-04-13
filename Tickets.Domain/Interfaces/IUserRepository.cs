using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;

namespace Tickets.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Esto me permite buscar usuarios por email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Usuario> GetUserByEmail(string email);

        /// <summary>
        /// Para crear un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="usuario">Informacion del usuario</param>
        /// <returns>Devuelve un usuario creado en la base y con un Id</returns>
        Task<Usuario> CreateUser(Usuario usuario);

        /// <summary>
        /// Para agregar un usuario a un rol específico, lo que es útil para gestionar permisos y accesos dentro de la aplicación.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<Usuario> AddToRoleAsync(Usuario usuario, string roleName);

        /// <summary>
        /// Par validar que la contraseña pertenece al usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(string userId, string password);

        Task<bool> UserExists(string email);

        Task<List<string>> GetUserRoles(string email);


    }
}
