using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface ICoffeeRepository : IBaseRepository<Coffee>
{
    /// <summary>
    /// Получить кофе по названию
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns>Кофе</returns>
    public Task<Coffee> GetByNameAsync(string nameCoffe);
    
    /// <summary>
    /// Добавить кофе в кофемашину
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Coffee> AddCoffeeByMachineAsync(Coffee coffee, Machine machine);
    
    /// <summary>
    /// Удалить кофе из кофемашины
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Coffee> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine);
}