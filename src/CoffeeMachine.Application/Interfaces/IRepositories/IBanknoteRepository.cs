using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IBanknoteRepository : IBaseRepository<Banknote>
{
    /// <summary>
    /// Получение банкнот по номиналу 
    /// </summary>
    /// <param name="par"></param>
    /// <returns>Банкноту</returns>
    public Task<Banknote> GetByParAsync(int par);
}