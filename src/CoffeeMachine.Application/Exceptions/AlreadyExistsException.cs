namespace CoffeeMachine.Infrastructure.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string name, object key) 
        : base($"Entity \"{name}\" ({key}) already exists.")
    {
        
    }
}