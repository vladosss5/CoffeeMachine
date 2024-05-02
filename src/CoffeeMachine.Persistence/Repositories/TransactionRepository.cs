using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

/// <summary>
/// Репозиторий транзакций.
/// </summary>
public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public TransactionRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить список транзакций по типу.
    /// </summary>
    /// <param name="type">Тип транзакции.</param>
    /// <returns>Список транзакций.</returns>
    public async Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type)
    {
        return await _dataContext.Transactions.Where(t => t.IsPayment == type).ToListAsync();
    }

    /// <summary>
    /// Получить список транзакций по заказу.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <returns>Список транзакций.</returns>
    public async Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(Order order)
    {
        return await _dataContext.Transactions.Where(t => t.Order == order).ToListAsync();
    }
}