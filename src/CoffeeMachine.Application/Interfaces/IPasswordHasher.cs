namespace CoffeeMachine.Application.Interfaces;

public interface IPasswordHasher
{
    public  Task<string> GenerateHashAsync(string password);
    public Task<bool> ValidateHashAsync(string password, string hash);
}