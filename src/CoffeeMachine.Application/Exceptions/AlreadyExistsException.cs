namespace CoffeeMachine.Application.Exceptions;

/// <summary>
/// Уже существует.
/// </summary>
public class AlreadyExistsException : Exception
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="name">Название сущности.</param>
    /// <param name="key">Уникальное поле сущности.</param>
    public AlreadyExistsException(string name, object key) 
        : base($"Entity {name} with key ({key}) already exists.")
    { }
}