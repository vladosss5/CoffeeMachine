using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface ICoffeeService : IBaseService<Coffee>
{
    public Task<Coffee> GetByName(string nameCoffe);
}