using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;

namespace CoffeeMachine.Persistence.Repositories;

public class TransactionRepository : IBaseRepository<Transaction>, ITransactionRepository
{
    private readonly MyDbContext _dbContext;

    public TransactionRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Transaction> GetByIdAsynk(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetAllAsynk()
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> AddAsynk(Transaction entity)
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