using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<User> GetByLoginAsync(string login);
}