using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    /// <summary>
    /// Получить кофемашину по id
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns>Кофемашина</returns>
    public Task<Machine> GetBySerialNumberAsync(string serialNumber);
}