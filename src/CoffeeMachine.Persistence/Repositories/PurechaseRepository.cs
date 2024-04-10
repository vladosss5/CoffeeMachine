using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;

namespace CoffeeMachine.Persistence.Repositories;

public class PurechaseRepository : IBaseRepository<Purchase>, IPurechaseRepository
{
    private readonly MyDbContext _dbContext;

    public PurechaseRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Purchase> GetByIdAsynk(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Purchase>> GetAllAsynk()
    {
        throw new NotImplementedException();
    }

    public Task<Purchase> AddAsynk(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<Purchase> Update(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Purchase entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByCoffee(Coffee coffee)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByMachine(Machine machine)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByStatus(string status)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetByDate(DateTime dateStart, DateTime dateEnd)
    {
        throw new NotImplementedException();
    }
}