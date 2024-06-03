using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

public interface IUserService
{
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User> GetUserByIdAsync(int id);
    public Task<User> CreateUserAsync(User user);
    public Task<User> UpdateUserAsync(User user);
    public Task DeleteUserAsync(int id);
    public Task<string> Login(string login, string password);
}