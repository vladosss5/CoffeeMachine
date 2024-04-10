using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;

namespace CoffeeMachine.Persistence.Services;

public class CoffeeService : IBaseService<Coffee>, ICoffeeService
{
    private readonly ICoffeeRepository _coffeeRepository;
    
    public CoffeeService(ICoffeeRepository coffeeRepository)
    {
        _coffeeRepository = coffeeRepository;
    }
    
    public Task<Coffee> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Coffee>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> Add(Coffee entity)
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