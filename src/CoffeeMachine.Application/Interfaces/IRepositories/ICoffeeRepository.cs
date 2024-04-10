using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface ICoffeeRepository : IBaseRepository<Coffee>
{
    public Task<Coffee> GetByNameAsync(string nameCoffe);
}