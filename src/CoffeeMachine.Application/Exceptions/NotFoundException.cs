namespace CoffeeMachine.Application.Exceptions;

/// <summary>
/// Не существует.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Контруктор класса.
    /// </summary>
    /// <param name="name">Название сущности.</param>
    /// <param name="key">Уникальное поле сущности.</param>
    public NotFoundException(string name, object key)
        : base($"Entity {name} with key ({key}) not found.")
    { }
}