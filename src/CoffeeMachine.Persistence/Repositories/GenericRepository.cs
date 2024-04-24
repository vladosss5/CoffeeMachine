using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;


/// <summary>
/// Обобщённый класс для CRUD операций.
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T> : IBaseRepository<T> where T : class
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly DataContext _dbContext;

    public GenericRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Получить список записей объекта.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Добавить запись объекта.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> AddAsync(T entity)
    {
        var result = await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.TrySaveChangesToDbAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновить запись объекта.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> UpdateAsync(T entity)
    {
        var result = _dbContext.Set<T>().Update(entity);
        await _dbContext.TrySaveChangesToDbAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удалить запись объекта.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(T entity)
    {
         _dbContext.Set<T>().Remove(entity);
         await _dbContext.TrySaveChangesToDbAsync();
         return true;
    }
}