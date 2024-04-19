using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

public interface IAdminService
{
    /// <summary>
    /// Получить список всех кофемашин
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Machine>> GetAllMachinesAsync();
    
    /// <summary>
    /// Создать новую кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> CreateNewMachineAsync(Machine machine);
    
    /// <summary>
    /// Удалить кофемашину
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<bool> DeleteMachineAsync(Machine machine);
    
    /// <summary>
    /// Получить список банкнот в кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine);
    
    /// <summary>
    /// Добавить банкноты в кофемашину
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> AddBanknotesToMachineAsync(List<Banknote> banknotes, Machine machine);
    
    /// <summary>
    /// Вычесть банкноты из кофемашины
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> SubtractBanknotesFromMachineAsync(List<Banknote> banknotes, Machine machine);
    
    /// <summary>
    /// Добавить кофе в кофемашину
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> AddCoffeeToMachineAsync(Coffee coffee, Machine machine);
    
    /// <summary>
    /// Удалить кофе из кофемашины
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine);
    
    /// <summary>
    /// Получить список кофе
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Coffee>> GetAllCoffeesAsync();
    
    /// <summary>
    /// Создать новый кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<Coffee> CreateNewCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Удалить кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<bool> DeleteCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Изменить кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<Coffee> UpdateCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
}