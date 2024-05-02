using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Репозиторий заказов.
/// </summary>
public interface IOrderRepository : IBaseRepository<Order>
{
    /// <summary>
    /// Получить полную информацию о заказе по Id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Заказ.</returns>
    public Task<Order> GetOrderByIdAsyncIcludeOtherEntities(long id);
    
    /// <summary>
    /// Получить список заказов по кофе.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <returns>Спсисок заказов.</returns>
    public Task<IEnumerable<Order>> GetByCoffeeAsync(Coffee coffee);
    
    /// <summary>
    /// Получить список заказов по кофемашине.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Список заказов.</returns>
    public Task<IEnumerable<Order>> GetByMachineAsync(Machine machine);
    
    /// <summary>
    /// Получить список заказов по статусу.
    /// </summary>
    /// <param name="status">Статус.</param>
    /// <returns>Список заказов.</returns>
    public Task<IEnumerable<Order>> GetByStatusAsync(string status);
    
    /// <summary>
    /// Получить список заказов за период.
    /// </summary>
    /// <param name="dateStart">Дата начала периода.</param>
    /// <param name="dateEnd">Дата окончания периода.</param>
    /// <returns>Список заказов.</returns>
    public Task<IEnumerable<Order>> GetByDateAsync(DateTime dateStart, DateTime dateEnd);
}