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
        
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="accountService">Сервис аутентификации пользоваетля.</param>
        public AccountController()
        {
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
                {"grant_type", "password"},
                {"client_id", "backend"},
                {"username", loginRequest.Login},
                {"password", loginRequest.Password},
                {"client_secret", "DaC67CWcTHYU8b73BGgt7gfU0OA9YUSn"},
                {"scope", "roles"}
            };
            
            var response = await client.PostAsync("http://localhost:8282/realms/MyRealm/protocol/openid-connect/token",
                new FormUrlEncodedContent(reqestKeycloak));
            var responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
            var token = (string)responseString["access_token"];
            
            return Ok(token);
        }
    }
}