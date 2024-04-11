namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

/// <summary>
/// Базовый  репозиторий для CRUD операций
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Получение объекта по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Запрошеный объект</returns>
    public Task<T> GetByIdAsync(long id);
    
    /// <summary>
    /// Получение всех объектов
    /// </summary>
    /// <returns>Коллекция запрошенных объектов</returns>
    public Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Добавление объектов
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Добавленный объект</returns>
    public Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Обновление объектов
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Обновлённый объект</returns>
    public Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// Удаление объектов
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>true или ошибку</returns>
    public Task<bool> DeleteAsync(T entity);
}