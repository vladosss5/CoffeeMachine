namespace CoffeeMachine.API.DTOs.Account;

/// <summary>
/// Модель авторизации.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
}