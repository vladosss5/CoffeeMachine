using CoffeeMachine.Domain.Models;

namespace CoffeeMachine.Infrastructure.Interfaces.IServices;

public interface IMachineService : IBaseService<Machine>
{
    public Task<Machine> GetBySerialNumber(string serialNumber);
}