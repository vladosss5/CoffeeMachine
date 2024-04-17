namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Получение объекта по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Запрошеный объект</returns>
    public Task<T> GetByIdAsync(long id);
    
    /// <summary>
    /// Получение списка всех объектов
    /// </summary>
    /// <returns>Коллекция запрошенных объектов</returns>
    public Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Добавление объекта
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Добавленный объект</returns>
    public Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Обновление объекта
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Обновлённый объект</returns>
    public Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// Удаление объекта
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>true или ошибку</returns>
    public Task<bool> DeleteAsync(T entity);
}