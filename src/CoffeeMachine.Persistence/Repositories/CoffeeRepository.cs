using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class CoffeeRepository : GenericRepository<Coffee>, ICoffeeRepository
{
    private readonly DataContext _dataContext;

    public CoffeeRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить кофе по названию
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        return await _dataContext.Coffees.FirstOrDefaultAsync(x => x.Name == nameCoffe);
    }

    /// <summary>
    /// Получить кофе доступные кофемашине.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetCoffeesFromMachineAsync(Machine machine)
    {
        return await _dataContext.CoffeesToMachines
            .Where(cm => cm.Machine == machine)
            .Select(cm => cm.Coffee)
            .ToListAsync();
    }
}