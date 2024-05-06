using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Репозиторий кофемашины.
/// </summary>
public interface IMachineRepository : IBaseRepository<Machine>
{
    /// <summary>
    /// Получить кофкмашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber">Серийный номер</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> GetBySerialNumberAsync(string serialNumber);
    
    /// <summary>
    /// Проверить есть ли кофе в кофемашине. True - есть, False - нет.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <param name="coffee">Кофе.</param>
    /// <returns>bool</returns>
    public Task<bool> CheckCoffeeInMachineAsync(Machine machine, Coffee coffee);
    
    /// <summary>
    /// Добавить кофе в кофемашину.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> AddCoffeeInMachineAsync(Coffee coffee, Machine machine);
    
    /// <summary>
    /// Удалить кофе из кофемашины.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine);
}