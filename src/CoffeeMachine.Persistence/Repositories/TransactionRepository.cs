using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly DataContext _dataContext;

    public TransactionRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить список транзакций по типу
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type)
    {
        return await _dataContext.Transactions.Where(t => t.IsPayment == type).ToListAsync();
    }

    /// <summary>
    /// Получить список странзакций по заказу
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(Order order)
    {
        return await _dataContext.Transactions.Where(t => t.Order == order).ToListAsync();
    }
}