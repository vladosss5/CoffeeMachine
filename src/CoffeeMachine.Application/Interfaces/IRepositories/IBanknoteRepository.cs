using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

public interface IBanknoteRepository : IBaseRepository<Banknote>
{
    /// <summary>
    /// Получение банкнот по номиналу 
    /// </summary>
    /// <param name="par"></param>
    /// <returns>Банкноту</returns>
    public Task<Banknote> GetByNominalAsync(int par);

    /// <summary>
    /// Получение банкнот в автомате
    /// </summary>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<Banknote>> GetBanknotesByMachineAsync(Machine machine);
    
    /// <summary>
    /// Добавить банкноты в автомат
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<BanknoteToMachine>> AddBanknotesToMachineAsync(IEnumerable<Banknote> banknotes, Machine machine);
    
    
    /// <summary>
    /// Выдать банкноты из автомата
    /// </summary>
    /// <param name="banknotes"></param>
    /// <param name="machine"></param>
    /// <returns></returns>
    public Task<IEnumerable<BanknoteToMachine>> SubtractBanknotesFromMachineAsync(IEnumerable<Banknote> banknotes, Machine machine);
}