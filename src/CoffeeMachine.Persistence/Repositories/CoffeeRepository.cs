using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class CoffeeRepository : ICoffeeRepository
{
    private readonly DataContext _dbContext;

    public CoffeeRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Получить кофе по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Coffee> GetByIdAsync(long id)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Id == id);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), id);
        
        return coffee;
    }

    /// <summary>
    /// Получить список кофе
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Coffee>> GetAllAsync()
    {
        return await _dbContext.Coffees.ToListAsync();
    }

    /// <summary>
    /// Добавить кофе
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
     public async Task<Coffee> AddAsync(Coffee entity)
    {
        var identity = await _dbContext.Coffees.AnyAsync(x => x.Name == entity.Name);
        
        if (identity != false)
            throw new AlreadyExistsException(nameof(Coffee), entity.Name);

        Coffee newCoffee = new Coffee()
        {
            Name = entity.Name,
            Price = entity.Price
        };
        
        await _dbContext.Coffees.AddAsync(newCoffee);
        await _dbContext.SaveChangesAsync();
        
        return newCoffee;
    }

    /// <summary>
    /// Изменить кофе
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Coffee> UpdateAsync(Coffee entity)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), entity.Id);
        
        coffee.Price = entity.Price;
        coffee.Name = entity.Name;
        
        _dbContext.Coffees.Update(coffee);
        await _dbContext.SaveChangesAsync();

        return coffee;
    }

    /// <summary>
    /// Удалить кофе
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DeleteAsync(Coffee entity)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Id == entity.Id);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), entity.Name);
        
        _dbContext.Coffees.Remove(coffee);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    /// <summary>
    /// Получить кофе по названию
    /// </summary>
    /// <param name="nameCoffe"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Name == nameCoffe);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), nameCoffe);
        
        return coffee;
    }

    /// <summary>
    /// Добавить кофе в кофемашину
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Coffee> AddCoffeeByMachineAsync(Coffee coffee, Machine machine)
    {
        var coffeeInMachine = new CoffeeToMachine()
        {
            Coffee = coffee,
            Machine = machine
        };
        
        await _dbContext.CoffeesToMachines.AddAsync(coffeeInMachine);
        await _dbContext.SaveChangesAsync();
        
        return coffeeInMachine.Coffee;
    }

    /// <summary>
    /// Удалить кофе из кофемашины
    /// </summary>
    /// <param name="coffee"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public async Task<Coffee> DeleteCoffeeFromMachineAsync(Coffee coffee, Machine machine)
    {
        var delitingCoffee = await _dbContext.CoffeesToMachines.FirstOrDefaultAsync(x =>
            x.Coffee == coffee && x.Machine == machine);

        if (delitingCoffee == null)
            throw new NotFoundException(nameof(Coffee), coffee.Name);
        
        _dbContext.CoffeesToMachines.Remove(delitingCoffee);
        await _dbContext.SaveChangesAsync();
        
        return delitingCoffee.Coffee;
    }
}