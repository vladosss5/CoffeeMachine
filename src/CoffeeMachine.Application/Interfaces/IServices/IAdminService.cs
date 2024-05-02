using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IServices;

/// <summary>
/// Сервис администратора.
/// </summary>
public interface IAdminService
{
    /// <summary>
    /// Получить кофемашину по Id.
    /// </summary>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> GetMachineByIdAsync(long machineId);
    
    /// <summary>
    /// Получить список всех кофемашин.
    /// </summary>
    /// <returns>Список кофемашин.</returns>
    public Task<IEnumerable<Machine>> GetAllMachinesAsync();
    
    /// <summary>
    /// Создать новую кофемашину.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Созданная кофемашина.</returns>
    public Task<Machine> CreateNewMachineAsync(Machine machine);
    
    /// <summary>
    /// Изменить кофемашину.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Изменённая кофемашина.</returns>
    public Task<Machine> UpdateMachineAsync(Machine machine);
    
    /// <summary>
    /// Удалить кофемашину.
    /// </summary>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    public Task DeleteMachineAsync(long machineId);
    
    /// <summary>
    /// Получить кофемашину по серийному номеру
    /// </summary>
    /// <param name="serialNumber">Серийному номер.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> GetMachineBySerialNumberAsync(string serialNumber);
    
    /// <summary>
    /// Обновить баланс кофемашины.
    /// </summary>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Баланс.</returns>
    public Task<int> UpdateBalanceAsync(long machineId);
    
    /// <summary>
    /// Добавить кофе в кофемашину.
    /// </summary>
    /// <param name="coffeeId">Идентификатор кофе.</param>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> AddCoffeeInMachineAsync(long coffeeId, long machineId);
    
    /// <summary>
    /// Удалить кофе из кофемашины.
    /// </summary>
    /// <param name="coffeeId">Идентификатор кофе.</param>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> DeleteCoffeeFromMachineAsync(long coffeeId, long machineId);
    
    /// <summary>
    /// Добавить банкноты в кофемашину.
    /// </summary>
    /// <param name="banknotesRequest">Список банкнот.</param>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotesRequest, long machineId);
    
    /// <summary>
    /// Извлечь банкноты из кофемашины.
    /// </summary>
    /// <param name="banknotesRequest">Список банкнот.</param>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Кофемашина.</returns>
    public Task<Machine> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotesRequest, long machineId);
    
    /// <summary>
    /// Получить список банкнот в кофемашине.
    /// </summary>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Список банкнот.</returns>
    public Task<IEnumerable<BanknoteToMachine>> GetBanknotesByMachineAsync(long machineId);

    /// <summary>
    /// Получить кофе по Id.
    /// </summary>
    /// <param name="coffeeId">Идентификатор кофе.</param>
    /// <returns>Кофе.</returns>
    public Task<Coffee> GetCoffeeByIdAsync(long coffeeId);
    
    /// <summary>
    /// Получить список кофе.
    /// </summary>
    /// <returns>Список кофе.</returns>
    public Task<IEnumerable<Coffee>> GetAllCoffeesAsync();
    
    /// <summary>
    /// Создать новый кофе.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <returns>Созданный кофе.</returns>
    public Task<Coffee> CreateNewCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Изменить кофе.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <returns>Измененный кофе.</returns>
    public Task<Coffee> UpdateCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Удалить кофе.
    /// </summary>
    /// <param name="coffeeId">Идентификатор кофе.</param>
    /// <returns></returns>
    public Task DeleteCoffeeAsync(long coffeeId);
    
    /// <summary>
    /// Получить кофе по названию.
    /// </summary>
    /// <param name="nameCoffe">Название кофе.</param>
    /// <returns>Кофе</returns>
    public Task<Coffee> GetByNameAsync(string nameCoffe);
    
    /// <summary>
    /// Получить список доступных кофе для машины.
    /// </summary>
    /// <param name="machineId">Идентификатор кофемашины.</param>
    /// <returns>Список кофе.</returns>
    public Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(long machineId);
    
    /// <summary>
    /// Получить заказ по Id.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <returns>Заказ.</returns>
    public Task<Order> GetOrderByIdAsync(long orderId);
    
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    /// <returns>Список заказов.</returns>
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
    
    /// <summary>
    /// Удалить заказ.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    public Task DeleteOrderAsync(long orderId);
    
    /// <summary>
    /// Вывести список транзакций.
    /// </summary>
    /// <returns>Список транзакций.</returns>
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    
    /// <summary>
    /// Получить транзакцию по Id.
    /// </summary>
    /// <param name="transactionId">Идентификатор транзакции.</param>
    /// <returns>Транзакция.</returns>
    public Task<Transaction> GetTransactionByIdAsync(long transactionId);
    
    /// <summary>
    /// Получить транзакции по типу. True - покупка, false - продажа.
    /// </summary>
    /// <param name="type">Тип.</param>
    /// <returns>Список транзакций.</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type);
    
    /// <summary>
    /// Получить транзакции покупки.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <returns>Список транзакций</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(long orderId);
}