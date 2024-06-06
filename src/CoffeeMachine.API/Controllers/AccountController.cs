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
        static HttpClient client = new HttpClient();

        private readonly IConfiguration _configuration;

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
            var conf = _configuration.GetSection("KeycloakLoginRequest").Get<KeycloakLoginRequest>();
            
            var reqestKeycloak = JObject.Parse(conf.ToString());
            reqestKeycloak["username"] = loginRequest.Login;
            reqestKeycloak["password"] = loginRequest.Password;
            
            var response = await client.PostAsync("http://localhost:8282/realms/MyRealm/protocol/openid-connect/token", 
                new StringContent(reqestKeycloak.ToString(), System.Text.Encoding.UTF8, "application/json"));
            
            var responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
            var token = (string)responseString["access_token"];
            
            return Ok(token);
        }
    }
}