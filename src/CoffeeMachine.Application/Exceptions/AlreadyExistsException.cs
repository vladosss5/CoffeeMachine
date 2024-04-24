namespace CoffeeMachine.Application.Exceptions;

public class AlreadyExistsException : Exception
{
    /// <summary>
    /// Ошибка: уже существует.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="key"></param>
    public AlreadyExistsException(string name, object key) 
        : base($"Entity {name} with key ({key}) already exists.")
    { }
}