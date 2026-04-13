using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Infrastructure.Identity;

namespace Tickets.Infrastructure.Mapping
{
    public static class UserMappingExtension
    {
        /// <summary>
        /// Pasa un usuario de dominio a la base de datos (AppIdentityUser)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static AppIdentityUser ToIdentityUser(this Usuario usuario)
        {

            return new AppIdentityUser
            {
                
                UserName = usuario.Email,
                Email = usuario.Email,
                PhoneNumber = usuario.Tel
            };
        }

        /// <summary>
        /// Pasa un usuario de la base a Dominio
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public static Usuario ToDomainUser(this AppIdentityUser identityUser)
        {
            return new Usuario
            {
                Id = Guid.Parse(identityUser.Id),
                Email = identityUser.Email,
                Tel = identityUser.PhoneNumber,
                FirtsName = identityUser.UserName, // Assuming UserName is used for FirstName
                LastName = "" // LastName is not available in AppIdentityUser, set it to an empty string or handle as needed
            };
        }
    }
}
