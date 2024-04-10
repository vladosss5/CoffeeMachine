namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface IBaseService<T> where T : class
{
    public Task<T> GetByIdAsync(long id);
    public Task<List<T>> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(T entity);
}