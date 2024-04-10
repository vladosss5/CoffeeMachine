using CoffeeMachine.Domain.Models;
using CoffeeMachine.Infrastructure.Exceptions;
using CoffeeMachine.Infrastructure.Interfaces.IRepositories;
using CoffeeMachine.Infrastructure.Interfaces.IServices;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class CoffeeRepository : IBaseRepository<Coffee>, ICoffeeRepository
{
    private readonly MyDbContext _dbContext;

    public CoffeeRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Coffee> GetByIdAsynk(long id)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Id == id);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), id);
        
        return coffee;
    }

    public async Task<IEnumerable<Coffee>> GetAllAsynk()
    {
        return await _dbContext.Coffees.ToListAsync();
    }

    public async Task<Coffee> AddAsynk(Coffee entity)
    {
        var identity = await _dbContext.Coffees.AnyAsync(x => x.Name == entity.Name);
        
        if (identity != false)
            throw new AlreadyExistsException(nameof(Coffee), entity.Name);

        Coffee newCoffee = new Coffee()
        {
            Name = entity.Name,
            Price = entity.Price,
            Size = entity.Size
        };
        
        await _dbContext.Coffees.AddAsync(newCoffee);
        await _dbContext.SaveChangesAsync();
        
        return newCoffee;
    }

    public async Task<Coffee> UpdateAsync(Coffee entity)
    {
        var coffee = await _dbContext.Coffees
            .FirstOrDefaultAsync(x => x.Name == entity.Name && (x.Size == entity.Size || x.Price == entity.Price));

        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), entity.Name);
        
        coffee.Price = entity.Price;
        coffee.Size = entity.Size;
        
        _dbContext.Coffees.Update(coffee);
        await _dbContext.SaveChangesAsync();

        return coffee;
    }

    public async Task<bool> DeleteAsync(Coffee entity)
    {
        var coffee = await _dbContext.Coffees
            .FirstOrDefaultAsync(x => x.Name == entity.Name && x.Price == entity.Price && x.Size == entity.Size);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), entity.Name);
        
        _dbContext.Coffees.Remove(coffee);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<Coffee> GetByNameAsync(string nameCoffe)
    {
        var coffee = await _dbContext.Coffees.FirstOrDefaultAsync(x => x.Name == nameCoffe);
        
        if (coffee == null)
            throw new NotFoundException(nameof(Coffee), nameCoffe);
        
        return coffee;
    }
}