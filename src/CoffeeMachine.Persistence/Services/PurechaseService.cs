using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class PurechaseService : IBaseService<Purchase>, IPurechaseService
{
    private readonly IPurechaseRepository _purechaseRepository;

    public PurechaseService(IPurechaseRepository purechaseRepository)
    {
        _purechaseRepository = purechaseRepository;
    }
    
    public Task<Purchase> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Purchase>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Purchase> Add(Purchase entity)
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