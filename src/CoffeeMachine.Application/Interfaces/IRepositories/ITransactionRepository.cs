using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    public Task<List<Transaction>> GetByTypeAsync(bool type);
    public Task<List<Transaction>> GetByPurchaseAsync(Purchase purchase);
}