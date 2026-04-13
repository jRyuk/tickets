using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces;

namespace Tickets.Infrastructure.Security
{
    public class JwtService : IJwtService
    {
        IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenarateToken(Usuario usuario, IList<string> roles)
        {
            //reclamaciones o caracteristicas que identifican al usuario
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.GivenName, usuario.FullName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["jwtLifeTime"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
