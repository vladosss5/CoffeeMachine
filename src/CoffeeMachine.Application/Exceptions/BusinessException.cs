namespace CoffeeMachine.Application.Exceptions;

/// <summary>
/// Бизнес исключение.
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public BusinessException() : base("Что-то пошло не так.")
    { }
}