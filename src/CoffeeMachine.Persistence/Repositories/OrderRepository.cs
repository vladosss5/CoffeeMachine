using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

/// <summary>
/// Репозиторий заказов.
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;
    
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public OrderRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить полную информацию о заказе по Id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Заказ.</returns>
    public async Task<Order> GetOrderByIdAsyncIcludeOtherEntities(long id)
    {
        return await _dataContext.Orders
            .Include(o => o.Machine)
            .Include(o => o.Coffee)
            .Include(o => o.Transactions)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    /// <summary>
    /// Получить список заказов по кофе.
    /// </summary>
    /// <param name="coffee">Кофе.</param>
    /// <returns>Спсисок заказов.</returns>
    public async Task<IEnumerable<Order>> GetByCoffeeAsync(Coffee coffee)
    {
        return await _dataContext.Orders.Where(o => o.Coffee == coffee).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов по кофемашине.
    /// </summary>
    /// <param name="machine">Кофемашина.</param>
    /// <returns>Список заказов.</returns>
    public async Task<IEnumerable<Order>> GetByMachineAsync(Machine machine)
    {
        return await _dataContext.Orders.Where(o => o.Machine == machine).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов по статусу.
    /// </summary>
    /// <param name="status">Статус.</param>
    /// <returns>Список заказов.</returns>
    public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
    {
        return await _dataContext.Orders.Where(o => o.Status == status).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов за период.
    /// </summary>
    /// <param name="dateStart">Дата начала периода.</param>
    /// <param name="dateEnd">Дата окончания периода.</param>
    /// <returns>Список заказов.</returns>
    public async Task<IEnumerable<Order>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        return await _dataContext.Orders
            .Where(p => p.DateTimeCreate >= dateStart && p.DateTimeCreate <= dateEnd)
            .ToListAsync();
    }
}