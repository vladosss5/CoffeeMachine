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
    
    public async Task<Purchase> GetByIdAsynk(long id)
    {
        var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(x => x.Id == id);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Purchase), id);
        
        return purchase;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsynk()
    {
        return await _dbContext.Purchases.ToListAsync();
    }

    public async Task<Purchase> AddAsynk(Purchase entity)
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
            Status = "entity.Status",
            Date = DateTime.UtcNow,
            IdCoffee = coffee.Id,
            IdMachine = machine.Id
        };

        await _dbContext.Purchases.AddAsync(newPurchase);
        await _dbContext.SaveChangesAsync();
        
        return newPurchase;
    }
    
    public async Task<Purchase> UpdateAsync(Purchase entity) // НЕ реализовано
    {
        var purchase = await _dbContext.Purchases
            .FirstOrDefaultAsync(x => x.Date == entity.Date && x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Purchase), entity);
        
        purchase.Status = entity.Status;
        
        return entity;
    }

    public async Task<bool> DeleteAsync(Purchase entity) // ХЕРОВО реализовано
    {
        var purchase = await _dbContext.Purchases
            .FirstOrDefaultAsync(x => x.Date == entity.Date && x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Purchase), entity);
        
        _dbContext.Purchases.Remove(purchase);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<Purchase>> GetByCoffeeAsync(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Purchase>> GetByMachineAsync(Machine machine)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Purchase>> GetByStatusAsync(string status)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Purchase>> GetByDateAsync(DateTime dateStart, DateTime dateEnd)
    {
        throw new NotImplementedException();
    }
}