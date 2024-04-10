using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface ICoffeeService : IBaseService<Coffee>
{
    public Task<Coffee> GetByNameAsync(string nameCoffe);
}