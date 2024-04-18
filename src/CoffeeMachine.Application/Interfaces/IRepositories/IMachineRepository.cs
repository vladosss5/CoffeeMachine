using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    /// <summary>
    /// Получить кофемашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns>Кофемашина</returns>
    public Task<Machine> GetBySerialNumberAsync(string serialNumber);
    
    /// <summary>
    /// Обновить баланс кофемашины
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<int> UpdateBalanceAsync(Machine machine);
    
    /// <summary>
    /// Получить список доступных для машины кофе
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<List<Coffee>> GetCoffeesFromMachineAsync(Machine machine);
    
    /// <summary>
    /// Проверить наличие кофе в машине
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<bool> CheckCoffeeInMachineAsync(Machine machine, Coffee coffee);
}