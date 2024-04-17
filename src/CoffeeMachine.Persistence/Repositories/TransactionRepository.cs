using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly DataContext _dbContext;

    public TransactionRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Получить транзакцию по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Transaction> GetByIdAsync(long id)
    {
        return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Получить список транзакций
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _dbContext.Transactions.ToListAsync();
    }


    /// <summary>
    /// Добавить транзакцию
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Transaction> AddAsync(Transaction entity)
    {
        var banknote = await _dbContext.Banknotes.FirstOrDefaultAsync(b => b.Nominal == entity.Banknote.Nominal);
        
        var newTransaction = new Transaction()
        {
            IsPayment = entity.IsPayment,
            Banknote = banknote,
            Order = entity.Order
        };
        
        await _dbContext.Transactions.AddAsync(newTransaction);
        await _dbContext.SaveChangesAsync();
        
        return newTransaction;
    }

    
    public async Task<Transaction> UpdateAsync(Transaction entity) // НЕЗАЧЕМ
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Transaction entity) // НЕЗАЧЕМ
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить список транзакций по типу
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<List<Transaction>> GetByTypeAsync(bool type)
    {
        return await _dbContext.Transactions.Where(t => t.IsPayment == type).ToListAsync();
    }

    /// <summary>
    /// Получить список странзакций по заказу
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public async Task<List<Transaction>> GetByOrderAsync(Order order)
    {
        return await _dbContext.Transactions.Where(t => t.Order == order).ToListAsync();
    }
}