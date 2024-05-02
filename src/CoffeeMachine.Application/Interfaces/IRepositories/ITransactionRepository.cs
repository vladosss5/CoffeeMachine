using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Репозиторий транзакции.
/// </summary>
public interface ITransactionRepository : IBaseRepository<Transaction>
{
    /// <summary>
    /// Получить список транзакций по типу.
    /// </summary>
    /// <param name="type">Тип транзакции.</param>
    /// <returns>Список транзакций.</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type);

    /// <summary>
    /// Получить список транзакций по заказу.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <returns>Список транзакций.</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(Order order);
}