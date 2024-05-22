namespace CoffeeMachine.Application.Exceptions;

/// <summary>
/// Ошибка авторизации.
/// </summary>
public class LoginException : Exception
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public LoginException() : base("Неверный логин или пароль.")
    { }
}