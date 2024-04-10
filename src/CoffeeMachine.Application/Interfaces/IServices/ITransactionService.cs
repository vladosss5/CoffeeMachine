using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface ITransactionService : IBaseService<Transaction>
{
    public Task<List<Transaction>> GetByType(bool type);
    public Task<List<Transaction>> GetByPurchase(Purchase purchase);
}