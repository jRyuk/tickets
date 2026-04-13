using System.Security.Claims;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces;

namespace Tickets.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userReposotory;
        private readonly IJwtService _jwtServic;
        public AuthService(IUserRepository userReposotory, IJwtService jwtService)
        { 
            _userReposotory = userReposotory;
            _jwtServic = jwtService;
        }

        public async Task<Usuario> RegisterUser(Usuario usuario)
        {

            await _userReposotory.CreateUser(usuario);

            return usuario;
        }

        public async Task<string> Login(string email, string password, bool rememberme)
        {
            var usuario = await _userReposotory.GetUserByEmail(email);

            if(usuario == null)
            {
                return "Credenciales invalidades";
            }

            var credencialesValidas = await _userReposotory.CheckPasswordAsync(usuario.Id.ToString(), password);

            if(!credencialesValidas)
            {
                return "Credenciales invalidades" ;
            }
           var roles = await _userReposotory.GetUserRoles(email);



            return  _jwtServic.GenarateToken(usuario, roles) ;
        }
    }
}
