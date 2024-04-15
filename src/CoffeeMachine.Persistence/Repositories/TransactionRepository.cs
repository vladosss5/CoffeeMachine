using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class TransactionRepository : IBaseRepository<Transaction>, ITransactionRepository
{
    private readonly DataContext _dbContext;

    public TransactionRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Transaction> GetByIdAsync(long id)
    {
        return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _dbContext.Transactions.ToListAsync();
    }

    public async Task<Transaction> AddAsync(Transaction entity)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(x => x.Nominal == entity.Banknote.Nominal);
        var purchase = await _dbContext.Orders.FirstOrDefaultAsync(x => 
            x.Date == entity.Order.Date && 
            x.Status == entity.Order.Status &&
            x.Machine.SerialNumber == entity.Order.Machine.SerialNumber);

        var newTransaction = new Transaction()
        {
            Type = entity.Type,
            // CountBanknotes = entity.CountBanknotes,
            Banknote = banknote,
            Order = purchase
        };
        
        await _dbContext.Transactions.AddAsync(newTransaction);
        await _dbContext.SaveChangesAsync();
        
        return newTransaction;
    }

    public async Task<Transaction> UpdateAsync(Transaction entity) // ЗАЧЕМ?
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Transaction entity)
    {
        var deletingTransaction = _dbContext.Transactions.FirstOrDefault(x => 
            x.Type == entity.Type &&
            x.Order.Date == entity.Order.Date);
        
        if (deletingTransaction == null)
            throw new NotFoundException(nameof(Transaction), entity);
        
        _dbContext.Transactions.Remove(deletingTransaction);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<Transaction>> GetByTypeAsync(bool type)
    {
        return await _dbContext.Transactions.Where(x => x.Type == type).ToListAsync();
    }

    public async Task<List<Transaction>> GetByPurchaseAsync(Order entity)
    {
        var purchase = await _dbContext.Orders.FirstOrDefaultAsync(x => 
            x.Date == entity.Date && 
            x.Machine.SerialNumber == entity.Machine.SerialNumber);
        
        if (purchase == null)
            throw new NotFoundException(nameof(Order), entity);
        
        return await _dbContext.Transactions
            .Where(t => t.Order == purchase)
            .ToListAsync();
    }
}