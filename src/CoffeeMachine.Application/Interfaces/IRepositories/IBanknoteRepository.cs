using CoffeeMachine.Core.Models;

namespace CoffeeMachine.Application.Interfaces.IRepositories;

/// <summary>
/// Репозиторий банкнот.
/// </summary>
public interface IBanknoteRepository : IBaseRepository<Banknote>
{
    /// <summary>
    /// Получить банкноту по номиналу.
    /// </summary>
    /// <param name="nominal">Номинал банкноты.</param>
    /// <returns>Банкнота с указанным номиналом.</returns>
    public Task<Banknote> GetByNominalAsync(int nominal);

    /// <summary>
    /// Получить список банкнот в машине.
    /// </summary>
    /// <param name="machine"> Кофемашина. </param>
    /// <returns> Список банкнот в кофемашине. </returns>
    public Task<IEnumerable<BanknoteToMachine>> GetBanknotesByMachineAsync(Machine machine);
}