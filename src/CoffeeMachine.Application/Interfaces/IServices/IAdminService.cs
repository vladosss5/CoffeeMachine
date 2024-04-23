using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

public interface IAdminService
{
    /// <summary>
    /// Получить кофемашину по Id.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<Machine> GetMachineByIdAsync(long machineId);
    
    /// <summary>
    /// Получить список всех кофемашин.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Machine>> GetAllMachinesAsync();
    
    /// <summary>
    /// Создать новую кофемашину.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> CreateNewMachineAsync(Machine machine);
    
    /// <summary>
    /// Изменить кофемашину.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<Machine> UpdateMachineAsync(Machine machine);
    
    /// <summary>
    /// Удалить кофемашину.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<bool> DeleteMachineAsync(long machineId);
    
    /// <summary>
    /// Получить кофемашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns></returns>
    public Task<Machine> GetMachineBySerialNumberAsync(string serialNumber);
    
    /// <summary>
    /// Обновить баланс кофемашины.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<int> UpdateBalanceAsync(long machineId);
    
    /// <summary>
    /// Добавить кофе в кофемашину.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<Machine> AddCoffeeInMachineAsync(long coffeeId, long machineId);
    
    /// <summary>
    /// Удалить кофе из кофемашины.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<Machine> DeleteCoffeeFromMachineAsync(long coffeeId, long machineId);
    
    /// <summary>
    /// Добавить банкноты в автомат.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<Machine> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotes, long machineId);
    
    /// <summary>
    /// Выдать банкноты из автомата.
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<Machine> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, long machineId);
    
    /// <summary>
    /// Получить список банкнот в кофемашине.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(long machineId);

    /// <summary>
    /// Получить кофе по Id.
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <returns></returns>
    public Task<Coffee> GetCoffeeByIdAsync(long coffeeId);
    
    /// <summary>
    /// Получить список кофе.
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Coffee>> GetAllCoffeesAsync();
    
    /// <summary>
    /// Создать новый кофе.
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<Coffee> CreateNewCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Изменить кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public Task<Coffee> UpdateCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Удалить кофе
    /// </summary>
    /// <param name="coffeeId"></param>
    /// <returns></returns>
    public Task<bool> DeleteCoffeeAsync(long coffeeId);
    
    /// <summary>
    /// Получить кофе по названию.
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns>Кофе</returns>
    public Task<Coffee> GetByNameAsync(string nameCoffe);
    
    /// <summary>
    /// Получить список доступных кофе для машины.
    /// </summary>
    /// <param name="machineId"></param>
    /// <returns></returns>
    public Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(long machineId);
    
    /// <summary>
    /// Получить заказ по Id.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Task<Order> GetOrderByIdAsync(long orderId);
    
    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
    
    /// <summary>
    /// Удалить заказ
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Task<bool> DeleteOrderAsync(long orderId);
    
    /// <summary>
    /// Получить транзакции по типу.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Список транзакций</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type);
    
    /// <summary>
    /// Получить транзакции покупки.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Список транзакций</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(Order order);
}