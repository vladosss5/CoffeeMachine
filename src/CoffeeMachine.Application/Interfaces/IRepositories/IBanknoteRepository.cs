using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IBanknoteRepository : IBaseRepository<Banknote>
{
    /// <summary>
    /// Получение банкнот по номиналу.
    /// </summary>
    /// <param name="nominal"></param>
    /// <returns>Банкноту</returns>
    public Task<Banknote> GetByNominalAsync(int nominal);

    /// <summary>
    /// Получение банкнот в автомате.
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<BanknoteToMachine>> GetBanknotesByMachineAsync(Machine machine);
}