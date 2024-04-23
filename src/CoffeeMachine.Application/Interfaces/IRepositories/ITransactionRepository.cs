using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    /// <summary>
    /// Получить транзакции по типу.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Список транзакций</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(bool type);
    
    /// <summary>
    /// Получить транзакции покупки.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Список транзакций</returns>
    public Task<IEnumerable<Transaction>> GetTransactionsByOrderAsync(Order order);
}