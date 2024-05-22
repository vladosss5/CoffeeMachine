using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CoffeeMachine.API.DTOs.Account;
using CoffeeMachine.API.DTOs.User;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeMachine.API.Controllers
{
    /// <summary>
    /// Контроллер аутентификации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Сервис аутентификации пользоваетля.
        /// </summary>
        private readonly IAccountService _accountService;
    
        /// <summary>
        /// Сервис автомаппера.
        /// </summary>
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="accountService">Сервис аутентификации пользоваетля.</param>
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="loginRequest">Модель авторизации.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var user = await _accountService.Login(loginRequest.Login, loginRequest.Password);
            
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            // Get the values from the GenerateTokenSettings section
            var issuer = configuration["GenerateTokenSettings:Issuer"];
            var audience = configuration["GenerateTokenSettings:Audience"];
            var secret = configuration["GenerateTokenSettings:Secret"];

            var claims = new List<Claim> {new Claim(ClaimTypes.Name, loginRequest.Login) };
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)), // время действия 2 минуты
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature));

            var loginResponse = new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                User = _mapper.Map<UserDto>(user)
            };
            
            return Ok(loginResponse);
        }
    }
}