using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    public Task<List<Transaction>> GetByType(bool type);
    public Task<List<Transaction>> GetByPurchase(Purchase purchase);
}