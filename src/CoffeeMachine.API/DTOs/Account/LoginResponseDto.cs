using CoffeeMachine.API.DTOs.User;

namespace CoffeeMachine.API.DTOs.Account;

public class LoginResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
}