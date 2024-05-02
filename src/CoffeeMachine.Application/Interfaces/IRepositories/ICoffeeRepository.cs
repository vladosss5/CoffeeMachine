using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Репозиторий кофе.
/// </summary>
public interface ICoffeeRepository : IBaseRepository<Coffee>
{
    /// <summary>
    /// Получить кофе по названию
    /// </summary>
    /// <param name="nameCoffe">Название кофе.</param>
    /// <returns>Кофе с указанным названием.</returns>
    public Task<Coffee> GetByNameAsync(string nameCoffe);
    
    /// <summary>
    /// Получить кофе доступные кофемашине.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Список кофе.</returns>
    public Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine);
}