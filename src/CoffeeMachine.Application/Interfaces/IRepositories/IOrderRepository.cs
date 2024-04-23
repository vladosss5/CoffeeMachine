using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    /// <summary>
    /// Получить список покупок
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns>Список покупок</returns>
    public Task<IEnumerable<Order>> GetByCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Получить покупки по кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns>Список покупок</returns>
    public Task<IEnumerable<Order>> GetByMachineAsync(Machine machine);
    
    /// <summary>
    /// Получить покупки по статусу оплаты
    /// </summary>
    /// <param name="status"></param>
    /// <returns>Список покупок</returns>
    public Task<IEnumerable<Order>> GetByStatusAsync(string status);
    
    /// <summary>
    /// Получить покупки в определённый период
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <returns>Список покупок</returns>
    public Task<IEnumerable<Order>> GetByDateAsync(DateTime dateStart, DateTime dateEnd);
}