using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;

namespace Tickets.Domain.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Para generar un token de acceso a recursos
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        string GenarateToken(Usuario usuario, IList<string> roles);
    }
}
