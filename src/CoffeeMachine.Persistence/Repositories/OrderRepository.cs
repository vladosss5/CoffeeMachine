using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class OrderRepository : IBaseRepository<Order>, IOrderRepository
{
    private readonly DataContext _dbContext;

    public OrderRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Order> GetByIdAsync(long id)
    {
        var purchase = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Order), id);
        
        return purchase;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order> AddAsync(Order entity)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Name == entity.Coffee.Name);
        
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => 
            x.SerialNumber == entity.Machine.SerialNumber);
        
        var identity = await _dbContext.Orders
            .AnyAsync(x => x.Date == entity.Date);
        
        if (identity != false)
            throw new AlreadyExistsException(nameof(Order), entity.Status);

        Order newOrder = new Order()
        {
            Status = entity.Status,
            Date = DateTime.UtcNow,
            Coffee = coffee,
            Machine = machine
        };

        await _dbContext.Orders.AddAsync(newOrder);
        await _dbContext.SaveChangesAsync();
        
        return newOrder;
    }
    
    public async Task<Order> UpdateAsync(Order entity)
    {
        var purchase = await _dbContext.Orders.FirstOrDefaultAsync(x => 
            x.Date == entity.Date && 
            x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Order), entity);
        
        purchase.Status = entity.Status;
        
        _dbContext.Orders.Update(purchase);
        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<bool> DeleteAsync(Order entity)
    {
        var purchase = await _dbContext.Orders.FirstOrDefaultAsync(x => 
            x.Date == entity.Date && 
            x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Order), entity);
        
        _dbContext.Orders.Remove(purchase);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<Order>> GetByCoffeeAsync(Coffee entity)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Name == entity.Name);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), entity);
        
        return await _dbContext.Orders
            .Where(p => p.Coffee == coffee)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByMachineAsync(Machine entity)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => 
            x.SerialNumber == entity.SerialNumber);

        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity);
        
        return await _dbContext.Orders
            .Where(p => p.Machine == machine)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByStatusAsync(string status)
    {
        return await _dbContext.Orders
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        return await _dbContext.Orders
            .Where(p => p.Date >= dateStart && p.Date <= dateEnd)
            .ToListAsync();
    }
}