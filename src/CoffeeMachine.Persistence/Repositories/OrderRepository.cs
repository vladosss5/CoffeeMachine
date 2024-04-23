using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly DataContext _dbContext;

    public OrderRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получить список заказов по кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetByCoffeeAsync(Coffee coffee)
    {
        return await _dbContext.Orders.Where(o => o.Coffee == coffee).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов по кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetByMachineAsync(Machine machine)
    {
        return await _dbContext.Orders.Where(o => o.Machine == machine).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов по статусу
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
    {
        return await _dbContext.Orders.Where(o => o.Status == status).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов за период
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        return await _dbContext.Orders
            .Where(p => p.DateTimeCreate >= dateStart && p.DateTimeCreate <= dateEnd)
            .ToListAsync();
    }
}