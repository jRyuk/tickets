using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets.Application.Services;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces;
using Tickets.DTOs;

namespace Tickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            Usuario newUser = new Usuario()
            {
                Email = registerUserDto.Email,
                FirtsName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Tel = registerUserDto.Phone,
                Password = registerUserDto.Password
            };

            var result = await _authService.RegisterUser(newUser);
            return Ok("Registro exitoso");
        }

        [HttpPost("login")]
     
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

           var result =  await _authService.Login(loginDto.Email, loginDto.Password, loginDto.RememberMe);

            return Ok(new { Token = result });
        }


    }
}
