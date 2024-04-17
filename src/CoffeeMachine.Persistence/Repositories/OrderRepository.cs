using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _dbContext;

    public OrderRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Order> GetByIdAsync(long id)
    {
        var purchase = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Order), id);
        
        return purchase;
    }

    /// <summary>
    /// Получить список заказ
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Order> AddAsync(Order entity)
    {
        Order newOrder = new Order()
        {
            Status = entity.Status,
            DateTimeCreate = DateTime.UtcNow,
            Coffee = entity.Coffee,
            Machine = entity.Machine
        };

        await _dbContext.Orders.AddAsync(newOrder);
        await _dbContext.SaveChangesAsync();
        
        newOrder.Transactions = entity.Transactions;
        
        return newOrder;
    }

    /// <summary>
    /// Обновить статус заказа
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Order> UpdateAsync(Order entity)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => 
            x.DateTimeCreate == entity.DateTimeCreate && 
            x.Machine == entity.Machine);
        
        if (order == null)
            throw new NotFoundException(nameof(Order), entity);
        
        order.Status = entity.Status;
        
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        
        return entity;
    }
    
    /// <summary>
    /// Удалить заказ
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> DeleteAsync(Order entity)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(x => 
            x.DateTimeCreate == entity.DateTimeCreate && 
            x.Machine == entity.Machine &&
            x.Status == entity.Status);
        
        if (order == null)
            throw new NotFoundException(nameof(Order), entity);
        
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    /// <summary>
    /// Получить список заказов по кофе
    /// </summary>
    /// <param name="coffee"></param>
    /// <returns></returns>
    public async Task<List<Order>> GetByCoffeeAsync(Coffee coffee)
    {
        return await _dbContext.Orders.Where(o => o.Coffee == coffee).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов по кофемашине
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<List<Order>> GetByMachineAsync(Machine machine)
    {
        return await _dbContext.Orders.Where(o => o.Machine == machine).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов по статусу
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<Order>> GetByStatusAsync(string status)
    {
        return await _dbContext.Orders.Where(o => o.Status == status).ToListAsync();
    }

    /// <summary>
    /// Получить список заказов за период
    /// </summary>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <returns></returns>
    public async Task<List<Order>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        return await _dbContext.Orders
            .Where(p => p.DateTimeCreate >= dateStart && p.DateTimeCreate <= dateEnd)
            .ToListAsync();
    }
}