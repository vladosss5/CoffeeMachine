using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IRepositories;

public interface IMachineRepository : IBaseRepository<Machine>
{
    public Task<Machine> GetBySerialNumber(string serialNumber);
}