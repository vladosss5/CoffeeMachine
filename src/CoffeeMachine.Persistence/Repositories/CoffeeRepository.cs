using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;
using CoffeeMachine.Persistence.Data.Context;

namespace CoffeeMachine.Persistence.Repositories;

public class CoffeeRepository : IBaseRepository<Coffee>, ICoffeeRepository
{
    private readonly MyDbContext _dbContext;

    public CoffeeRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Coffee> GetByIdAsynk(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Coffee>> GetAllAsynk()
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> AddAsynk(Coffee entity)
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> Update(Coffee entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Coffee entity)
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> GetByName(string nameCoffe)
    {
        throw new NotImplementedException();
    }
}