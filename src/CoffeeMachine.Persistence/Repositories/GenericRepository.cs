using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Application.Interfaces.IRepositories;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Repositories;


/// <summary>
/// Обобщённый класс для CRUD операций.
/// </summary>
/// <typeparam name="T">Модель данных.</typeparam>
public class GenericRepository<T> : IBaseRepository<T> where T : class
{
    /// <summary>
    /// <inheritdoc cref="DataContext"/>
    /// </summary>
    private readonly DataContext _dataContext;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="dataContext">Контекст для работы с базой данных.</param>
    public GenericRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Сущность.</returns>
    public async Task<T> GetByIdAsync(long id)
    {
        return await _dataContext.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Получить список записей объекта.
    /// </summary>
    /// <returns>Список сущностей.</returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dataContext.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Добавить запись объекта.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Добавленная сущность.</returns>
    public async Task<T> AddAsync(T entity)
    {
        var result = await _dataContext.Set<T>().AddAsync(entity);
        await _dataContext.TrySaveChangesToDbAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновить запись объекта.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Обновленная сущность.</returns>
    public async Task<T> UpdateAsync(T entity)
    {
        var result = _dataContext.Set<T>().Update(entity);
        await _dataContext.TrySaveChangesToDbAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удалить запись объекта.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public async Task DeleteAsync(T entity)
    {
         _dataContext.Set<T>().Remove(entity);
         await _dataContext.TrySaveChangesToDbAsync();
    }
}