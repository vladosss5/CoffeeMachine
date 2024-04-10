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
    
    public Task<Transaction> GetByIdAsynk(int id)
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