namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : class
{
    public Task<T> GetByIdAsync(long id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(T entity);
}