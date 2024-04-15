using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    /// <summary>
    /// Получить транзакции по типу
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Список транзакций</returns>
    public Task<List<Transaction>> GetByTypeAsync(bool type);
    
    /// <summary>
    /// Получить транзакции покупки
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Список транзакций</returns>
    public Task<List<Transaction>> GetByPurchaseAsync(Order order);
}