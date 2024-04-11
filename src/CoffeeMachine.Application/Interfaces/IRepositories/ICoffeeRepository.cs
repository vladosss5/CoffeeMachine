using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface ICoffeeRepository : IBaseRepository<Coffee>
{
    /// <summary>
    /// Получение кофе по названию
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns>Кофе</returns>
    public Task<Coffee> GetByNameAsync(string nameCoffe);
}