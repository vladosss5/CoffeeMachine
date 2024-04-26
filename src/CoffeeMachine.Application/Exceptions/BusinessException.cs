namespace CoffeeMachine.Application.Exceptions;

public class BusinessException : Exception
{
    public BusinessException() : base("Что-то пошло не так.")
    { }
}