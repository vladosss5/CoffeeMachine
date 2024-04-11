using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class PurchaseRepository : IBaseRepository<Purchase>, IPurchaseRepository
{
    private readonly MyDbContext _dbContext;

    public PurchaseRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Purchase> GetByIdAsync(long id)
    {
        var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(x => x.Id == id);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Purchase), id);
        
        return purchase;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _dbContext.Purchases.ToListAsync();
    }

    public async Task<Purchase> AddAsync(Purchase entity)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => 
            x.Name == entity.Coffee.Name && 
            x.Size == entity.Coffee.Size);
        
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => 
            x.SerialNumber == entity.Machine.SerialNumber);
        
        var identity = await _dbContext.Purchases
            .AnyAsync(x => x.Date == entity.Date);
        
        if (identity != false)
            throw new AlreadyExistsException(nameof(Purchase), entity.Status);

        Purchase newPurchase = new Purchase()
        {
            Status = entity.Status,
            Date = DateTime.UtcNow,
            IdCoffee = coffee.Id,
            IdMachine = machine.Id
        };

        await _dbContext.Purchases.AddAsync(newPurchase);
        await _dbContext.SaveChangesAsync();
        
        return newPurchase;
    }
    
    public async Task<Purchase> UpdateAsync(Purchase entity)
    {
        var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(x => 
            x.Date == entity.Date && 
            x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Purchase), entity);
        
        purchase.Status = entity.Status;
        
        _dbContext.Purchases.Update(purchase);
        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<bool> DeleteAsync(Purchase entity)
    {
        var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(x => 
            x.Date == entity.Date && 
            x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Purchase), entity);
        
        _dbContext.Purchases.Remove(purchase);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<Purchase>> GetByCoffeeAsync(Coffee entity)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => 
            x.Name == entity.Name && 
            x.Size == entity.Size && 
            x.Size == entity.Size);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), entity);
        
        return await _dbContext.Purchases
            .Where(p => p.IdCoffee == coffee.Id)
            .ToListAsync();
    }

    public async Task<List<Purchase>> GetByMachineAsync(Machine entity)
    {
        var machine = await _dbContext.Machines.FirstOrDefaultAsync(x => 
            x.SerialNumber == entity.SerialNumber);

        if (machine == null)
            throw new NotFoundException(nameof(Machine), entity);
        
        return await _dbContext.Purchases
            .Where(p => p.IdMachine == machine.Id)
            .ToListAsync();
    }

    public async Task<List<Purchase>> GetByStatusAsync(string status)
    {
        return await _dbContext.Purchases
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<List<Purchase>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        return await _dbContext.Purchases
            .Where(p => p.Date >= dateStart && p.Date <= dateEnd)
            .ToListAsync();
    }
}