using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface ICoffeeRepository : IBaseRepository<Coffee>
{
    /// <summary>
    /// Получить кофе по названию.
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns>Кофе</returns>
    public Task<Coffee> GetByNameAsync(string nameCoffe);
    
    /// <summary>
    /// Получить список доступных для машины кофе.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine);
}