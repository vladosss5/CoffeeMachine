namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface IBaseService<T> where T : class
{
    public Task<T> GetById(int id);
    public Task<List<T>> GetAll();
    public Task<T> Add(T entity);
    public Task<T> Update(T entity);
    public Task<bool> Delete(T entity);
}