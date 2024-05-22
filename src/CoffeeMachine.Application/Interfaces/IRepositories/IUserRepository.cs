using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IUserRepository
{
    public Task<User> GetByLoginAndPasswordAsync(string login, string password);
}