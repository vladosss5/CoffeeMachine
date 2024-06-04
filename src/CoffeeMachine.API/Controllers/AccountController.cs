using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using CoffeeMachine.API.DTOs.Account;
using CoffeeMachine.API.DTOs.User;
using CoffeeMachine.Application.Interfaces.IServices;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
        private readonly IUserService _userService;
    
        /// <summary>
        /// Сервис автомаппера.
        /// </summary>
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="accountService">Сервис аутентификации пользоваетля.</param>
        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="loginRequest">Модель авторизации.</param>
        /// <returns>Токен и данные пользователя.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var token = await _userService.Login(loginRequest.Login, loginRequest.Password);
            
            return Ok(token);
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserCreateRequestDto registerRequest)
        {
            var user = new User
            {
                Login = registerRequest.Login,
                PasswordHash = registerRequest.Password,
                Role = new Role { Id = registerRequest.RoleId }
            };
            
            var createdUser = await _userService.CreateUserAsync(user);
            
            return Ok(createdUser);
        }
    }
}