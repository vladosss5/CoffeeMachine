using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    /// <summary>
    /// Получить кофемашину по серийному номеру.
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns>Кофемашина</returns>
    public Task<Machine> GetBySerialNumberAsync(string serialNumber);
    
    /// <summary>
    /// Обновить баланс кофемашины.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<int> UpdateBalanceAsync(Machine machine);
    
    /// <summary>
    /// Проверить наличие кофе в машине.
    /// </summary>
    /// <param name="machine"></param>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<bool> CheckCoffeeInMachineAsync(Machine machine, Coffee coffee);
    
    /// <summary>
    /// Добавить кофе в кофемашину.
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> AddCoffeeInMachineAsync(Coffee coffee, Machine machine);
    
    /// <summary>
    /// Удалить кофе из кофемашины.
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine);
    
    /// <summary>
    /// Добавить банкноты в автомат.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotes, Machine machine);
    
    /// <summary>
    /// Выдать банкноты из автомата.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, Machine machine);
}