using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class BanknoteRepository : IBaseRepository<Banknote>, IBanknoteRepository
{
    private readonly MyDbContext _dbContext;

    
    public BanknoteRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<Banknote> GetByIdAsynk(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Banknote>> GetAllAsynk()
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> AddAsynk(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> Update(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Banknote entity)
    {
        throw new NotImplementedException();
    }

    public Task<Banknote> GetByPar(int par)
    {
        throw new NotImplementedException();
    }
}