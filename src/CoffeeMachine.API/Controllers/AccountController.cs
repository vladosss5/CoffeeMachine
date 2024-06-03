using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using CoffeeMachine.API.DTOs.Account;
using CoffeeMachine.API.DTOs.User;
using CoffeeMachine.Application.Interfaces.IServices;
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
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var user = await _accountService.Login(loginRequest.Login, loginRequest.Password);
            var client = new HttpClient();
            string url = "http://localhost:8080/realms/TestRealm/protocol/openid-connect/token";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", "test-client"),
                new KeyValuePair<string, string>("username", "test"),
                new KeyValuePair<string, string>("password", "test"),
                new KeyValuePair<string, string>("client_secret", "NzVbv4eJ8ncga6cdunKFdl1HMXfvwrSz"),
                new KeyValuePair<string, string>("scope", "roles")
            });

            var response = await client.PostAsync(url, content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringResponse)["access_token"];
            
            var loginResponse = new LoginResponseDto
            {
                User = user,
                Token = token
            };
            
            return Ok(loginResponse);
        }
        
        
    }
}