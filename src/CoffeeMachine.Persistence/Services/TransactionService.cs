using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class TransactionService : IBaseService<Transaction>, ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    
    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    public Task<Transaction> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> AddAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> UpdateAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetByTypeAsync(bool type)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetByPurchaseAsync(Purchase purchase)
    {
        throw new NotImplementedException();
    }
}