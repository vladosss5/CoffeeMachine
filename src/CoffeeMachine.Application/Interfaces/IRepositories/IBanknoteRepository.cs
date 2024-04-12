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

    /// <summary>
    /// Получение банкнот в автомате
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine);
}