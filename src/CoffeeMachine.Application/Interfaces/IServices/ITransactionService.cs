using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface ITransactionService : IBaseService<Transaction>
{
    public Task<List<Transaction>> GetByTypeAsync(bool type);
    public Task<List<Transaction>> GetByPurchaseAsync(Purchase purchase);
}