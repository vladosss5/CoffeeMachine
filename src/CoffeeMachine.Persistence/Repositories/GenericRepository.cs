using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;

public class GenericRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DataContext _dbContext;

    public GenericRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync(long id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);

        if (entity == null)
            throw new NotFoundException(nameof(T), id);
        
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        var result = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.TrySaveChangesToDbAsync();
        return result.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var result = _dbContext.Set<T>().Update(entity);
        await _dbContext.TrySaveChangesToDbAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
         _dbContext.Set<T>().Remove(entity);
         await _dbContext.TrySaveChangesToDbAsync();
         return true;
    }
}