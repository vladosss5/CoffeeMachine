namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IBaseRepository<T> where T : class
{
    public Task<T> GetByIdAsynk(int id);
    public Task<IEnumerable<T>> GetAllAsynk();
    public Task<T> AddAsynk(T entity);
    public Task<T> Update(T entity);
    public Task<bool> Delete(T entity);
}