using CoffeeMachine.API.DTOs;
using CoffeeMachine.API.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
        /// Http клиент.
        /// </summary>
        static HttpClient client = new HttpClient();
        
        /// <summary>
        /// Конфигурация проекта.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="configuration">Конфигурация проекта.</param>
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        /// <summary>
        /// Авторизация.
        /// </summary>
        /// <param name="loginRequest">Модель авторизации.</param>
        /// <returns>Токен и данные пользователя.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var reqestKeycloak = new Dictionary<string, string>
            {
                {"grant_type", _configuration["KeycloakLoginRequest:grant_type"]},
                {"client_id", _configuration["KeycloakLoginRequest:client_id"]},
                {"username", loginRequest.Login},
                {"password", loginRequest.Password},
                {"client_secret", _configuration["KeycloakLoginRequest:client_secret"]},
                {"scope", _configuration["KeycloakLoginRequest:scope"]}
            };
            
            var response = await client.PostAsync("http://localhost:8282/realms/MyRealm/protocol/openid-connect/token",
                new FormUrlEncodedContent(reqestKeycloak));
            
            var responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
            var token = (string)responseString["access_token"];
            
            return Ok(token);
        }
    }
}