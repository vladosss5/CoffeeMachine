using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class PurechaseRepository : IBaseRepository<Purchase>, IPurechaseRepository
{
    private readonly MyDbContext _dbContext;

    public PurechaseRepository(MyDbContext dbContext)
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

    public async Task<Purchase> AddAsynk(Purchase entity) // НЕ реализовано
    {
        var identity = await _dbContext.Purchases
            .AnyAsync(x => x.Status == entity.Status && x.Date == entity.Date);
        
        if (identity != false)
            throw new AlreadyExistsException(nameof(Purchase), entity.Status);
        
        // Purchase newPurchase = new Purchase()
        // {
        //     Status = 
        // }
        
        return entity;
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