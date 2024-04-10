namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : class
{
    public Task<T> GetByIdAsynk(long id);
    public Task<IEnumerable<T>> GetAllAsynk();
    public Task<T> AddAsynk(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(T entity);
}