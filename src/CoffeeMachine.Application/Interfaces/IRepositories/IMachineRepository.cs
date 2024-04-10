using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    public Task<Machine> GetBySerialNumberAsync(string serialNumber);
}