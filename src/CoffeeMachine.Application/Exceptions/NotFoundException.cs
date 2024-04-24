namespace CoffeeMachine.Application.Exceptions;

public class NotFoundException : Exception
{
    /// <summary>
    /// Ошибка: не существует.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="key"></param>
    public NotFoundException(string name, object key)
        : base($"Entity {name} with key ({key}) not found.")
    { }
}