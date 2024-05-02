namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Бащовый репозиторий.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Сущность.</returns>
    public Task<T> GetByIdAsync(long id);
    
    /// <summary>
    /// Получить список записей объектов.
    /// </summary>
    /// <returns>Список сущностей.</returns>
    public Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Добавить запись объекта.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns>Добавленная сущность.</returns>
    public Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Обновление записи объекта
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Обновлённый объект</returns>
    public Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// Удалить запись объекта.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    public Task DeleteAsync(T entity);
}