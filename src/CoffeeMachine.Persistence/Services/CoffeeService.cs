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
    
    public Task<Coffee> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Coffee>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> AddAsync(Coffee entity)
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> UpdateAsync(Coffee entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Coffee entity)
    {
        throw new NotImplementedException();
    }

    public Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        throw new NotImplementedException();
    }
}