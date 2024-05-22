using CoffeeMachine.API.DTOs.User;

namespace CoffeeMachine.API.DTOs.Account;

public class LoginResponseDto
{
    public string Token { get; set; }
    public Core.Models.User User { get; set; }
}