using CoffeeMachine.Application.Interfaces;

namespace CoffeeMachine.Persistence;

public class PasswordHasher : IPasswordHasher
{
    public async Task<string> GenerateHashAsync(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public async Task<bool> ValidateHashAsync(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}