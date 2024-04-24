using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class CoffeeRepository : GenericRepository<Coffee>, ICoffeeRepository
{
    private readonly DataContext _dbContext;

    public CoffeeRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получить кофе по названию
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        return await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Name == nameCoffe);
    }

    /// <summary>
    /// Получить кофе доступные кофемашине.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine)
    {
        return await _dbContext.CoffeesToMachines
            .Where(cm => cm.Machine == machine)
            .Select(cm => cm.Coffee)
            .ToListAsync();
    }
}