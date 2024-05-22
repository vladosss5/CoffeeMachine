using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

public interface IAccountService
{
    public Task<User> Login(string login, string password);
}