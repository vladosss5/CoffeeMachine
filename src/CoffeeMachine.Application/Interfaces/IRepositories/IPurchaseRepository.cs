using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IPurchaseRepository : IBaseRepository<Purchase>
{
    /// <summary>
    /// Получить список покупок
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns>Список покупок</returns>
    public Task<List<Purchase>> GetByCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Получить покупки по кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns>Список покупок</returns>
    public Task<List<Purchase>> GetByMachineAsync(Machine machine);
    
    /// <summary>
    /// Получить покупки по статусу оплаты
    /// </summary>
    /// <param name="status"></param>
    /// <returns>Список покупок</returns>
    public Task<List<Purchase>> GetByStatusAsync(string status);
    
    /// <summary>
    /// Получить покупки в определённый период
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <returns>Список покупок</returns>
    public Task<List<Purchase>> GetByDateAsync(DateTime dateStart, DateTime dateEnd);
}