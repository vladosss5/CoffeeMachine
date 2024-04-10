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
    
    public Task<Transaction> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> Add(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> Update(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetByType(bool type)
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetByPurchase(Purchase purchase)
    {
        throw new NotImplementedException();
    }
}