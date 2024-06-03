namespace CoffeeMachine.API.DTOs.User;

public class UserCreateRequestDto
{
     public string Login { get; set; }
     public string Password { get; set; }
     public long RoleId { get; set; }
}